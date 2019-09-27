using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public ApplicationController(IUserRepository userRepository, 
                                     IGroupRepository groupRepository, 
                                     IPaymentRepository paymentRepository,
                                     IExpenseRepository expenseRepository) {
            _userRepository = userRepository;
            _groupRepository = groupRepository;
            _paymentRepository = paymentRepository;
            _expenseRepository = expenseRepository;
        }

        [HttpPost("login")]
        public async Task<ActionResult<User>> LoginAsync([FromQuery] string name) {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("Invalid name");
            User user = await _userRepository.GetByNameAsync(name);
            if (user == null)
                user = await _userRepository.CreateAsync(user);

            return user;
        }

        [HttpGet("user/{id}/groups")]
        public async Task<ActionResult<IEnumerable<Group>>> GetGroupsAsync(int id) {
            List<Group> groups = (await _userRepository.GetGroupsAsync(id)).ToList();
            return groups;
        }

        [HttpGet("user/{userId}/group/{groupId}")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersOfGroupAsync(int userId, int groupId) {
            List<User> users = (await _groupRepository.GetUsersAsync(groupId, userId)).ToList();
            return users;
        }
    }
}