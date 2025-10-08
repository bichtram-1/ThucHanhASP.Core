using Microsoft.AspNetCore.Http;
using System.Text.Json;
namespace SportsStoreWebApp.Extensions
{
    public static class SessionExtensions
    {
        public static void SetObjectAsJson(this ISession session, string
key, object value)
    }
}