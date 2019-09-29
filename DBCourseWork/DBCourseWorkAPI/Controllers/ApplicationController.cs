using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs;
using AutoMapper;
using DataLayer.Abstraction;
using DataLayer.Entities;
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
        public async Task<ActionResult<UserDto>> LoginAsync(string name) {
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

        [HttpGet("user/{userId}/group/{groupId}")]
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
    }
}