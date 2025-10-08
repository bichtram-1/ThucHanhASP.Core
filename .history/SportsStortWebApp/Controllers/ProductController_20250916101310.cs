// // Controllers/ProductController.cs
// using Microsoft.AspNetCore.Mvc;
// using SportsStore.Domain.Abstract;
// using SportsStore.Domain.Models;
// using System.Linq;
// public class ProductController : Controller
// {
// private readonly IProductRepository _repository;
// public ProductController(IProductRepository repository)
// {
// _repository = repository;
// }
// public IActionResult List(string? category = null) // Tham số
// //category để lọc, có thể null
// {
// var products = _repository.Products.Where(p => category == null ||p.Category == category).ToList();
// ViewBag.CurrentCategory = category ?? "Tất cả sản phẩm";
// return View(products);
// }
// [Route("chi-tiet/{id:int}")]
// public IActionResult Details(int id)
// {
// var product = _repository.Products.FirstOrDefault(p =>p.ProductID == id);
// if (product == null)
// {
// return NotFound();
// }
// return View(product);
// }
// }
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
public class ProductController: Controller
{
    private readonly IConfiguration _configuration;
    public ProductController (IConfiguration configuration)
    {
        _configuration=configuration;

    }
    public IActionResult List(){
        string defaultLogLevel =_configuration["Logging:LogLevel:Default"];
        Console.WriteLine($Default Log Level: {defaultLogLevel});
        Output :Information
    }
}
