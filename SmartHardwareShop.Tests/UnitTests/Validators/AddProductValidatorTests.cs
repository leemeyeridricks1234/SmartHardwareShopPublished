using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using SmartHardwareShop.API.Controllers;
using SmartHardwareShop.API.DTOs;
using SmartHardwareShop.API.Models;
using SmartHardwareShop.API.Repositories;
using SmartHardwareShop.API.Responses;
using SmartHardwareShop.API.Validators;
using System;
using System.Collections.Generic;

namespace SmartHardwareShop.UnitTests.Validators
{
    public class AddProductValidatorTests
    {
        AddProductValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new AddProductValidator();
        }

        private static Product GetValidProduct()
        {
            var product = new Product();
            product.Category = "Category 1";
            product.Description = "Description 1";
            product.Image = "image.png";
            product.Name = "Name 1";
            product.Price = 100;
            product.Quantity = 5;
            product.RRP = 105;
            return product;
        }

        [Test]
        public void Given_ValidProduct_NoErrors()
        {
            //given
            Product product = GetValidProduct();

            //when
            _validator.Validate(product);

            //then
            Assert.Pass();
        }

        [Test]
        public void Given_InvalidCategory_ErrorExpected()
        {
            //given
            Product product = GetValidProduct();
            product.Category = "";

            //when
            Assert.Throws<ApplicationException>(() => _validator.Validate(product));
        }

        [Test]
        public void Given_InvalidName_ErrorExpected()
        {
            //given
            Product product = GetValidProduct();
            product.Name = "";

            //when
            Assert.Throws<ApplicationException>(() => _validator.Validate(product));
        }
    }
}