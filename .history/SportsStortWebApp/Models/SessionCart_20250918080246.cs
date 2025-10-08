using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SportsStore.Domain.Models;
using SportsStoreWebApp.Extensions;
using System.Text.Json.Serialization;

namespace SportsStoreWebApp.Models
{
    public class SessionCart : Cart
    {
        public static Cart GetCart(IServiceProvider services)
        {
            ISession? session =
services.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Session
;
        }
    }
}