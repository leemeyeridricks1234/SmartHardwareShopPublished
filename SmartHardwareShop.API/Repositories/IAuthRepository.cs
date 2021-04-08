using SmartHardwareShop.API.Responses;

namespace SmartHardwareShop.API.Repositories
{
    public interface IAuthRepository
    {
        string Login(string username, string password);
    }
}