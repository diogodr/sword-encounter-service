using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using sword_encounter_service.Models;
using sword_encounter_service.Services;

namespace sword_encounter_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;

        public AuthController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public ActionResult<User> Login([FromBody]User user)
        {
            try
            {   
                var userAuth = _userService.Get(user.Email, user.Password);

                return userAuth;
            }
            catch (Exception ex)
            {
                return NotFound("E-mail ou senha inválidos");
            }
        }
    }
}
