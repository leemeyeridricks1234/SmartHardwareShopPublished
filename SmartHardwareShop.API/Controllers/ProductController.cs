using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartHardwareShop.API.DTOs;
using SmartHardwareShop.API.Models;
using SmartHardwareShop.API.Repositories;
using SmartHardwareShop.API.Responses;
using SmartHardwareShop.API.Validators;

namespace SmartHardwareShop.API.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductController(IProductRepository productRepository, IHttpContextAccessor httpContextAccessor)
        {
            this.productRepository = productRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

       //private string GetUserRole() => _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);

        // GET api/<ProductsController>
        [AllowAnonymous]
        [HttpGet("GetAllPaged")]
        public IActionResult GetAll([FromQuery] PaginationFilter filter)
        {
            var response = new ServiceResponse<List<Product>>();

            try
            {
                //TODO: validate

                //action
                var route = "";
                if (Request != null)
                    route = Request.Path.Value;

                var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
                var pagedResponse = productRepository.GetProducts(validFilter, route);

                if (pagedResponse == null || !pagedResponse.Success)
                {
                    pagedResponse.Success = false;
                    return BadRequest(pagedResponse);
                }

                return Ok(pagedResponse);
            }
            catch (Exception ex)
            {
                //todo: logging
                response.Success = false;
                response.Message = "Exception: " + ex.Message;
                return BadRequest(response);
            }
        }

        // GET api/<ProductsController>/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var response = new ServiceResponse<Product>();
            try
            {
                response.Data = productRepository.GetProduct(id);

                if (response.Data == null)
                {
                    response.Success = false;
                    response.Message = "Product could not be found.";
                    return BadRequest(response);
                }
                else
                {
                    response.Success = true;
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                //todo: logging
                response.Success = false;
                response.Message = "Exception: " + ex.Message;
                return BadRequest(response);
            }
        }

        // POST api/<ProductsController>
        [HttpPost]
        public IActionResult Post([FromBody] Product product)
        {
            var response = new ServiceResponse<Product>();
            response.Success = true;
            try
            {
                //validate
                var validator = new AddProductValidator();
                validator.Validate(product);

                //defaults
                product.DateAdded = DateTime.Now;
                product.UserId = GetUserId();

                //action
                int productId = productRepository.AddProduct(product);

                //check
                if (productId == 0)
                {
                    response.Success = false;
                    response.Message = "Failed to save Product.";
                    return BadRequest(response);
                }

                product.Id = productId;
                response.Data = product;
                return Ok(response);
            }
            catch (Exception ex)
            {
                //todo: logging
                response.Success = false;
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Product product)
        {
            var response = new ServiceResponse<Product>();
            try
            {
                response.Success = true;

                //TODO: validation

                //defaults
                product.UserId = GetUserId();

                //action
                var ret = productRepository.UpdateProduct(product);

                response.Data = ret;
                return Ok(response);
            }
            catch (Exception ex)
            {
                //todo: logging
                response.Success = false;
                response.Message = "Exception: " + ex.Message;
                return BadRequest(response);
            }
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int productId)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                //TODO: validation


                //action
                var ret = productRepository.DeleteProduct(productId);
                response.Success = true;
                response.Data = ret;
                return Ok(response);
            }
            catch (Exception ex)
            {
                //todo: logging
                response.Success = false;
                response.Message = "Exception: " + ex.Message;
                return BadRequest(response);
            }
        }
    }
}
