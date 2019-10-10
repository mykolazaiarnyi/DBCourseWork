using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs;
using AutoMapper;
using DataLayer.Abstraction;
using DataLayer.Entities;
using DBCourseWorkAPI.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [Route("api")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IExpenseRepository _expenseRepository;
        private readonly IMapper _mapper;

        public ApplicationController(IUserRepository userRepository, 
                                     IGroupRepository groupRepository, 
                                     IPaymentRepository paymentRepository,
                                     IExpenseRepository expenseRepository,
                                     IMapper mapper) {
            _userRepository = userRepository;
            _groupRepository = groupRepository;
            _paymentRepository = paymentRepository;
            _expenseRepository = expenseRepository;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> LoginAsync([FromBody] string name) {
            User user = await _userRepository.GetByNameAsync(name);
            if (user == null)
                user = await _userRepository.CreateAsync(user);

            return _mapper.Map<UserDto>(user);
        }

        [HttpGet("user/{id}/groups")]
        public async Task<ActionResult<IEnumerable<GroupDto>>> GetGroupsAsync(int id) {
            var groups = await _userRepository.GetGroupsAsync(id);
            
            return _mapper.Map<IEnumerable<GroupDto>>(groups).ToList();
        }

        [HttpGet("user/{userId}/group/{groupId}/users")]
        public async Task<ActionResult<IEnumerable<UserWithBalanceDto>>> GetUsersOfGroupAsync(int userId, int groupId) {
            
            var users = await _groupRepository.GetUsersAsync(groupId, userId);

            var usersWithBalance = _mapper.Map<IEnumerable<User>, IEnumerable<UserWithBalanceDto>>(users, opts =>
                    opts.AfterMap(async (src, dest) => {
                        var d = dest as IEnumerable<UserWithBalanceDto>;
                        foreach(var i in d) 
                            i.Balance = await _userRepository.GetBalanceAsync(userId, i.Id, groupId);
                    })
                );

            return usersWithBalance.ToList();
        }

        [HttpGet("user/{userId}/group/{groupId}/expenses")]
        public async Task<ActionResult<IEnumerable<ExpenseDto>>> GetExpensesAsync(int userId, int groupId) {
            var expenses = await _expenseRepository.GetExpensesOfGroupAsync(groupId);
            var mappedExpenses = new List<ExpenseDto>();
            foreach (var i in expenses) {
                mappedExpenses.Add(new ExpenseDto() {
                    Amount = i.Amount,
                    Description = i.Description,
                    Time = i.Time,
                    ByUserName = i.ByUserId == userId ? "You" : (await _userRepository.GetByIdAsync(i.ByUserId)).Name
                });
            }
            return mappedExpenses;
        }

        [HttpGet("user/{userId}/group/{groupId}/payments")]
        public async Task<ActionResult<IEnumerable<PaymentDto>>> GetPaymentsAsync(int userId, int groupId) {
            var payments = await _paymentRepository.GetPaymentsOfUser(userId, groupId);
            var mappedPayments = new List<PaymentDto>();
            foreach (var i in payments) {
                mappedPayments.Add(new PaymentDto() {
                    Id = i.Id,
                    Description = i.Description,
                    Time = i.Time,
                    Amount = i.Amount,
                    ByUser = i.ByUserId == userId ? "You" : (await _userRepository.GetByIdAsync(i.ByUserId)).Name,
                    ForUser = i.ForUserId == userId ? "You" : (await _userRepository.GetByIdAsync(i.ForUserId)).Name,
                    Confirmed = i.Confirmed
                });
            }
            return mappedPayments;
        }

        [HttpPut("user")]
        public async Task<ActionResult> ChangeUserNameAsync(UserDto user) {
            var userEntity = await _userRepository.GetByIdAsync(user.Id);
            if (userEntity == null)
                return BadRequest();

            userEntity = await _userRepository.GetByNameAsync(user.Name);
            if (userEntity != null)
                return BadRequest();

            await _userRepository.UpdateAsync(_mapper.Map<User>(user));
            return Ok();
        }

        [HttpPost("user/{id}/groups")]
        public async Task<ActionResult<GroupDto>> CreateGroupAsync(int id, [FromBody] string name) {
            var group = await _groupRepository.CreateAsync(new Group() { Name = name });
            await _groupRepository.AddUserAsync(group.Id, id);
            return _mapper.Map<GroupDto>(group);
        }

        [HttpPost("user/{userId}/group/{groupId}")]
        public async Task<ActionResult> AddUserAsync(int userId, int groupId, [FromBody] string name) {
            var user = await _userRepository.GetByNameAsync(name);
            if (user == null || user.Id == userId)
                return BadRequest();

            var group = await _groupRepository.GetByIdAsync(groupId);
            if (group == null)
                return BadRequest();

            group.Users = await _groupRepository.GetUsersAsync(group.Id, userId);
            if (group.Users.Any(u => u.Id == user.Id))
                return BadRequest();

            await _groupRepository.AddUserAsync(group.Id, user.Id);

            return Ok();
        }

        [HttpPost("payment")]
        public async Task<ActionResult<PaymentDto>> AddPaymentAsync(PaymentDto payment) {
            throw new NotImplementedException();
        }

        //Add expense
        //Confirm payment
        //Delete group
    }
}