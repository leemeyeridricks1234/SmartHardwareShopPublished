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
using System.Web;
using SmartHardwareShop.API.Services;

namespace SmartHardwareShop.API.Controllers
{
    [Authorize(Roles = "customer")]
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICartRepository cartRepository;
        private readonly ICartService cartService;
        private readonly IProductRepository productRepository;

        public CartController(IHttpContextAccessor httpContextAccessor, ICartRepository cartRepository, 
            ICartService cartService,
            IProductRepository productRepository)
        {
            _httpContextAccessor = httpContextAccessor;

            this.cartRepository = cartRepository;
            this.cartService = cartService;
            this.productRepository = productRepository;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        //Get Current Cart Items
        [HttpGet("Get")]
        public IActionResult Get()
        {
            var response = new ServiceResponse<List<CartItem>>();

            try
            {
                //TODO: validate

                response.Data = cartRepository.GetCartItems(GetUserId());
                response.Success = true;

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

        //Get Current Cart Items
        [HttpGet("GetBasketSummary")]
        public IActionResult GetBasketSummary()
        {
            var response = new ServiceResponse<CartSummaryDto>();

            try
            {
                //TODO: validate
                var summary = new CartSummaryDto();

                var cartItems = cartRepository.GetCartItems(GetUserId());

                foreach (var item in cartItems)
                {
                    var product = productRepository.GetProduct(item.ProductId);

                    var summaryItem = new CartSummaryItemDto();
                    summaryItem.Price = product.Price;
                    summaryItem.ProductId = item.ProductId;
                    summaryItem.ProductName = product.Name;
                    summaryItem.Quantity = item.Quantity;

                    summary.Items.Add(summaryItem);
                }

                response.Data = summary;
                response.Success = true;

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

        [HttpPost("AddItem")]
        public IActionResult Post([FromBody] CartItemDto dto)
        {
            var response = new ServiceResponse<int>();
            response.Success = true;
            try
            {
                //validate

                //action
                var id = cartService.AddToCart(GetUserId(), dto.ProductId, dto.Quantity);

                //check
                if (id == 0)
                {
                    response.Success = false;
                    response.Message = "Failed to save Cart Item.";
                    return BadRequest(response);
                }

                response.Data = id;
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

        [HttpPost("Checkout")]
        public IActionResult PostCheckOut()
        {
            var response = new ServiceResponse<bool>();
            response.Success = true;
            try
            {
                //validate

                //action
                cartService.Checkout(GetUserId());

                response.Data = true;
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
    }
}
