using Microsoft.AspNetCore.Mvc;
using SmartHardwareShop.API.DTOs;
using SmartHardwareShop.API.Repositories;
using SmartHardwareShop.API.Responses;
using SmartHardwareShop.API.Validators;
using System;

namespace SmartHardwareShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;
        public AuthController(IAuthRepository authRepo)
        {
            _authRepo = authRepo;
        }

        [HttpPost("Login")]
        public IActionResult Login(UserLoginDto request)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            try
            {
                var validator = new AuthValidator();
                validator.Validate(request);

                response.Data = _authRepo.Login(request.Username, request.Password);
                if (!response.Success)
                {
                    return BadRequest(response);
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                //todo: logging
                response.Success = false;
                response.Message = "Exception: " + ex.Message;
                return BadRequest(response);
            }
        }
    }
}
