using SmartHardwareShop.API.DTOs;
using SmartHardwareShop.API.Models;
using SmartHardwareShop.API.Responses;
using System.Collections.Generic;

namespace SmartHardwareShop.API.Repositories
{
    public interface IProductRepository
    {
        PagedResponse<List<Product>> GetProducts(PaginationFilter validFilter, string route);
        Product GetProduct(int productId);
        int AddProduct(Product product);
        Product UpdateProduct(Product product);
        Product UpdateProductQuantity(int productId, int quantityToReduce);
        bool DeleteProduct(int productId);
    }
}