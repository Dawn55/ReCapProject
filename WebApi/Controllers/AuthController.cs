using Buisness.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("login")]
        public IActionResult Login(UserForLoginDto userForLoginDto)
        {
            var user = _authService.Login(userForLoginDto);
            if (!user.Success)
            {
                return BadRequest();
            }
           var result = _authService.CreateToken(user.Data);
            if (result.Success)
            {
                return Ok();
            }
            return BadRequest();
        }
        [HttpPost("register")]
        public IActionResult Register(UserForRegisterDto userForRegister)
        {
           //var exist = _authService.UserExist(userForRegister.Email);
           // if (!exist.Success)
           // {
           //     return BadRequest();
           // }
            var user = _authService.Register(userForRegister);
            if (!user.Success)
            {
                return BadRequest();
            }
           var result = _authService.CreateToken(user.Data);
            return Ok(result);
        }
    }
}
