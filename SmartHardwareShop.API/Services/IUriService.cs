using SmartHardwareShop.API.DTOs;
using System;

namespace SmartHardwareShop.API.Services
{
    public interface IUriService
    {
        Uri GetPageUri(PaginationFilter filter, string route);
    }
}