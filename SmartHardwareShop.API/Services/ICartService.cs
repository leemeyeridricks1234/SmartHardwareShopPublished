namespace SmartHardwareShop.API.Services
{
    public interface ICartService
    {
        int AddToCart(int userId, int productId, int quantity);
        void Checkout(int userId);
        void ClearCart(int userId);
    }
}