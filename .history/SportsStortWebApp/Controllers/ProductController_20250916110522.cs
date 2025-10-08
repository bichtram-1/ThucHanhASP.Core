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
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Configuration;
// public class ProductController: Controller
// {
//     private readonly IConfiguration _configuration;
//     public ProductController (IConfiguration configuration)
//     {
//         _configuration=configuration;

//     }
//     public IActionResult List(){
//         string defaultLogLevel =_configuration["Logging:LogLevel:Default"];
//         Console.WriteLine($Default Log Level: {defaultLogLevel});
//        var pagingSection._configuration.GetSection("PagingSettings");
//        int itemsPerPage=pagingSection.GetValue<int>("ItemsPerpage");
//        int maxPagesToShow=_configuration.GetValue<int>("PagingSettings:MaxPagesToShow");
//        Console.WriteLine($"Items per page: {itemsPerPage}, Max pages:{maxPagesToShow}")
//        return View();

//     }
// }

//ghi nhật ký ứng dụng:  bắt đầu từ trang 3 tuần 4
//phân trang:  bắt đầu từ trang 24 slide tuần 4
//use form trong core  và laravel thì khai báo (CSRF)

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SportsStore.Domain.Models;
using System;
public class ProductController: Controller
{
    private readonly ILogger<ProductController> _logger;
    public ProductController(ILogger<ProductController> logger)
    { 
        _logger=logger;

    }
    public IActionResult Create(){
        return View();
    }
    public IActionResult Create (Product NewProduct) 
    {
        _logger.LogInformation("Đang cố gắng tạo sản phẩm mới: {ProductName}",NewProduct.Name);
        if(ModelState.IsValid)
        {
            newProduct.ProductID=new Random().Next(1000,9999);
            _logger.LogInformation("Sản phẩm {ProductName} (ID:{ProductID} đã được tạo thành công.", newProduct.Name,newProduct.ProductID);
            TemData["SuccessMessage"]=$"sản phẩm"
        }
    }

}
//bx sau học quản lý trạng thái
//4 cách truyền từ controller sang view và ngược lại
//