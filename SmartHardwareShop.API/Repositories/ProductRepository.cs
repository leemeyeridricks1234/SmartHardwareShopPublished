using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SmartHardwareShop.API.DTOs;
using SmartHardwareShop.API.Models;
using SmartHardwareShop.API.Responses;
using SmartHardwareShop.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartHardwareShop.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDbContextFactory<SmartHardwareShopContext> contextFactory;
        private readonly IUriService uriService;

        public ProductRepository(IDbContextFactory<SmartHardwareShopContext> contextFactory,
            IUriService uriService)
        {
            this.contextFactory = contextFactory;
            this.uriService = uriService;
        }

        public Product GetProduct(int productId)
        {
            using (var context = contextFactory.CreateDbContext())
            {
                return context.Product.FirstOrDefault(x => x.Id == productId);
            }
        }

        public PagedResponse<List<Product>> GetProducts(PaginationFilter validFilter, string route)
        {
            using (var context = contextFactory.CreateDbContext())
            {
                var pagedData = context.Product
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToList();
                var totalRecords = context.Product.Count();
                var pagedReponse = CreatePagedReponse<Product>(pagedData, validFilter, totalRecords, uriService, route);
                return pagedReponse;
            }
        }

        public static PagedResponse<List<T>> CreatePagedReponse<T>(List<T> pagedData, PaginationFilter validFilter,
            int totalRecords, IUriService uriService, string route)
        {
            var response = new PagedResponse<List<T>>(pagedData, validFilter.PageNumber, validFilter.PageSize);
            var totalPages = ((double)totalRecords / (double)validFilter.PageSize);
            int roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
            response.NextPage =
                validFilter.PageNumber >= 1 && validFilter.PageNumber < roundedTotalPages
                ? uriService.GetPageUri(new PaginationFilter(validFilter.PageNumber + 1, validFilter.PageSize), route)
                : null;
            response.PreviousPage =
                validFilter.PageNumber - 1 >= 1 && validFilter.PageNumber <= roundedTotalPages
                ? uriService.GetPageUri(new PaginationFilter(validFilter.PageNumber - 1, validFilter.PageSize), route)
                : null;
            response.FirstPage = uriService.GetPageUri(new PaginationFilter(1, validFilter.PageSize), route);
            response.LastPage = uriService.GetPageUri(new PaginationFilter(roundedTotalPages, validFilter.PageSize), route);
            response.TotalPages = roundedTotalPages;
            response.TotalRecords = totalRecords;
            return response;
        }

        public int AddProduct(Product product)
        {
            using (var context = contextFactory.CreateDbContext())
            {
                var entity = context.Product.Add(product);
                var ret = context.SaveChanges();
                return entity.Entity.Id;
            }
        }

        public Product UpdateProduct(Product product)
        {
            var productFound = GetProduct(product.Id);
            if (productFound == null)
            {
                throw new ApplicationException("Product not found for ID = " + product.Id);
            }

            productFound.Name = product.Name;
            productFound.Description = product.Description;
            productFound.Category = product.Category;
            productFound.Image = product.Image;
            productFound.Quantity = product.Quantity;
            productFound.Price = product.Price;
            productFound.RRP = product.RRP;

            using (var context = contextFactory.CreateDbContext())
            {
                var entity = context.Product.Update(product);
                var ret = context.SaveChanges();
                return productFound;
            }
        }

        public Product UpdateProductQuantity(int productId, int quantityToReduce)
        {
            var productFound = GetProduct(productId);
            if (productFound == null)
            {
                throw new ApplicationException("Product not found for ID = " + productId);
            }
            if (productFound.Quantity < quantityToReduce)
            {
                throw new ApplicationException("Product quantity not enough = " + productFound.Quantity);
            }

            productFound.Quantity -= quantityToReduce;

            using (var context = contextFactory.CreateDbContext())
            {
                var entity = context.Product.Update(productFound);
                var ret = context.SaveChanges();
                return productFound;
            }
        }

        public bool DeleteProduct(int productId)
        {
            var productFound = GetProduct(productId);
            if (productFound == null)
            {
                throw new ApplicationException("Product not found for ID = " + productId);
            }

            using (var context = contextFactory.CreateDbContext())
            {
                var entity = context.Product.Remove(productFound);
                var ret = context.SaveChanges();
                return true;
            }
        }
    }
}
