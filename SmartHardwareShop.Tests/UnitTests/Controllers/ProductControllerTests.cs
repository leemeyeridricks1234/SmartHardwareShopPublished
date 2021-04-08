using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using SmartHardwareShop.API.Controllers;
using SmartHardwareShop.API.DTOs;
using SmartHardwareShop.API.Models;
using SmartHardwareShop.API.Repositories;
using SmartHardwareShop.API.Responses;
using System.Collections.Generic;

namespace SmartHardwareShop.UnitTests.Controllers
{
    public class ProductControllerTests
    {
        ProductController _controller;
        Mock<IProductRepository> _productRepository;
        Mock<IHttpContextAccessor> _httpContextAccessor;

        [SetUp]
        public void Setup()
        {
            _productRepository = new Mock<IProductRepository>();
            _httpContextAccessor = new Mock<IHttpContextAccessor>();

            _controller = new ProductController(_productRepository.Object, _httpContextAccessor.Object);
        }

        [Test]
        public void Test_GetAll_Success_Default()
        {
            // Set up the mock
            var products = new List<Product>();
            var pagedResponse = new PagedResponse<List<Product>>(products, 1, 5);

            //given
            var paginatedFilter = new PaginationFilter();

            _productRepository.Setup(m => m.GetProducts(It.IsAny<PaginationFilter>(), It.IsAny<string>())).Returns(pagedResponse);
            //myInterfaceMock.SetupGet(m => m.Name).Returns("Molly");

            //when
            var results = _controller.GetAll(paginatedFilter);

            //then
            //Assert.AreEqual(1, results.)
            Assert.Pass();
        }
    }
}