using SmartHardwareShop.API.Models;
using System;

namespace SmartHardwareShop.API.Validators
{
    public class AddProductValidator : Validator<Product>
    {
        protected override void Execute(Product dto)
        {
            if (String.IsNullOrEmpty(dto.Name))
            {
                Errors.Add("Name cannot be null or empty.");
            }
            if (String.IsNullOrEmpty(dto.Description))
            {
                Errors.Add("Description cannot be null or empty.");
            }
            if (String.IsNullOrEmpty(dto.Category))
            {
                Errors.Add("Category cannot be null or empty.");
            }
            if (dto.Price <= 0)
            {
                Errors.Add("Price must be positive amount.");
            }
            if (dto.RRP <= 0)
            {
                Errors.Add("RRP must be positive amount.");
            }
        }

    }
}
