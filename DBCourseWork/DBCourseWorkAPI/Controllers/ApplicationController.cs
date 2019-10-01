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
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("Invalid name");
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
            await _paymentRepository.
        }

        [HttpPut("user/{id}")]
        public async Task<ActionResult<bool>> ChangeUserNameAsync(int id) {
            throw new NotImplementedException();
        }

        [HttpPost("user/{id}/groups")]
        public async Task<ActionResult<GroupDto>> CreateGroupAsync(int id, [FromBody] string name) {
            throw new NotImplementedException();
        }

        [HttpPost("user/{userId}/group/{groupId}")]
        public async Task<ActionResult<bool>> AddUserAsync(int userId, int groupId, [FromBody] string name) {
            throw new NotImplementedException();
        }

        //Add payment
        //Add expense
        //Confirm payment
        //Delete group
    }
}