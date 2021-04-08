using SmartHardwareShop.API.DTOs;
using System;

namespace SmartHardwareShop.API.Validators
{
    public class AuthValidator : Validator<UserLoginDto>
    {
        protected override void Execute(UserLoginDto dto)
        {
            if (String.IsNullOrEmpty(dto.Username))
            {
                Errors.Add("Username cannot be null or empty.");
            }
            if (String.IsNullOrEmpty(dto.Password))
            {
                Errors.Add("Password cannot be null or empty.");
            }
        }

    }
}
