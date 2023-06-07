﻿using ItssProject.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace ItssProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IGetDataService _dataService;
        public UserController(IGetDataService dataService)
        {
            _dataService = dataService;
        }
        [HttpPost("login")]
        public IActionResult CheckUserInfor([FromBody] LoginRequestModel Model)
        {
            try
            {
                var userName = Model.UserName;
                var password = Model.Password;
                if (userName == null || password == null)
                {
                    return BadRequest("Please enter enough information");
                }
                if (password.Length < 8)
                {
                    return BadRequest("Password is not enough to characters");
                }
                var result = _dataService.CheckUserInformation(userName, password);
                if (result == false)
                {
                    return BadRequest("Information of user is wrong");
                }
                return Ok("Login is successfully");
            }
            catch
            {
                throw new Exception();
            }
        }
        public class LoginRequestModel
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }
    }
}





