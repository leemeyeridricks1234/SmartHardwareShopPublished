using SmartHardwareShop.API.Responses;
using System;
using System.Collections.Generic;

namespace SmartHardwareShop.API.Validators
{
    public abstract class Validator<T> : IValidator<T>
    {
        protected readonly List<string> Errors;
        protected ServiceResponse<T> ServiceResponse;

        public Validator()
        {
            Errors = new List<string>();
            ServiceResponse = new ServiceResponse<T>();
        }

        public void Validate(T dto)
        {
            var serviceResponse = new ServiceResponse<T>();
            serviceResponse.Success = true;
            serviceResponse.Data = dto;

            Execute(dto);

            if (Errors.Count > 0)
            {
                throw new ApplicationException("Validation Errors: " + FormatMessage(Errors));
            }
        }

        private string FormatMessage(List<string> errors)
        {
            string message = "";
            foreach (var error in errors)
            {
                message += error + "\n ";
            }
            return message;
        }

        protected abstract void Execute(T dto);
    }
}
