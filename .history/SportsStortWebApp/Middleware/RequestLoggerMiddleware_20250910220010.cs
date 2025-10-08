using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Diagnostics; // Dùng cho Debug.WriteLine
namespace SportsStoreWebApp.Middleware
{
public class RequestLoggerMiddleware
{
private readonly RequestDelegate _next;
public RequestLoggerMiddleware(RequestDelegate next)
{
_next = next;
}