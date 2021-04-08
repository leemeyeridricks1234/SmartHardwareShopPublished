using SmartHardwareShop.API.Responses;

namespace SmartHardwareShop.API.Validators
{
    public interface IValidator<T>
    {
        void Validate(T dto);
    }
}