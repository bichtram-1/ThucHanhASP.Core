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
//phân trang để load ít lại, đỡ tốn băng thông(dung lượng 3g):  bắt đầu từ trang 24 slide tuần 4
//use form trong core  và laravel thì khai báo (CSRF)

// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Logging;
// using SportsStore.Domain.Models;
// using System;
// public class ProductController: Controller
// {
//     private readonly ILogger<ProductController> _logger;
//     public ProductController(ILogger<ProductController> logger)
//     { 
//         _logger=logger;

//     }
//     public IActionResult Create(){
//         return View();
//     }
//     [HttpPost]
//     [ValidateAntiForgeryToken]
//     public IActionResult Create (Product NewProduct) 
//     {
//         _logger.LogInformation("Đang cố gắng tạo sản phẩm mới: {ProductName}",NewProduct.Name);
//         if(ModelState.IsValid)
//         {
//             newProduct.ProductID=new Random().Next(1000,9999);
//             _logger.LogInformation("Sản phẩm {ProductName} (ID:{ProductID} đã được tạo thành công.", newProduct.Name,newProduct.ProductID);
//             TemData["SuccessMessage"]=$"Sản phẩm `{newProduct.Name}`đã được tạo thành công với ID: {newProduct.ProductID}";
//         return RedirectToAction("List");
//         }
//         _logger.LogWarning("Tạo sản phẩm thất bại do trạng thái model không hợp lệ cho sản phẩm: {ProductName}", newProduct.Name);
//     foreach (var modelStateEntry in ModelState.Values)
// {
// foreach (var error in modelStateEntry.Errors)
// {
// _logger.LogError("Lỗi trạng thái Model: {ErrorMessage}", error.ErrorMessage);
// }
// }
// return View(newProduct);
//     }
// public IActionResult List()
// {
// // Lấy thông báo thành công từ TempData để hiển thị
// ViewBag.Message = TempData["SuccessMessage"];
// return View(); // Trả về view List.cshtml
// }
// // Hành động GET: /Product/ErrorExample
// // Minh họa việc ghi log lỗi khi có ngoại lệ (exception) xảy ra
// public IActionResult ErrorExample()
// {
// try
// {
// // Cố tình ném ra một ngoại lệ để minh họa
// throw new Exception("Đây là một lỗi mô phỏng cho mục đích ghi log.");
// }
// catch (Exception ex)
// {
// // Ghi log lỗi (mức Error) bao gồm cả thông tin chi tiết về ngoại lệ
// _logger.LogError(ex, "Một ngoại lệ chưa được xử lý đã xảy ra trong hành động ErrorExample.");
// }
// return View(); // Trả về view ErrorExample.cshtml
// }
// }

// //bx sau học quản lý trạng thái
// //4 cách truyền từ controller sang view và ngược lại
// //controller ko muốn xuất ra view thì dùng gì để hiện


// Controllers/ProductController.cs (hoặc AdminController.cs)
// Thêm các using cần thiết
using Microsoft.AspNetCore.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Models;
using Microsoft.Extensions.Logging;
using System.Linq;
// Giả sử bạn có IProductRepository được tiêm ở đây
public class AdminController : Controller // Hoặc AdminController
{
private readonly IProductRepository _repository;
private readonly ILogger<AdminController> _logger;
public AdminController(IProductRepository repository,
ILogger<AdminController> logger)
{
_repository = repository;
_logger = logger;
}
// Action để hiển thị form tạo/chỉnh sửa sản phẩm
public IActionResult Edit(int productId = 0)
{
Product? product = productId == 0 ? new Product() :
_repository.Products.FirstOrDefault(p => p.ProductID == productId);
if (product == null && productId != 0)
{_logger.LogWarning("Không tìm thấy sản phẩm có ID {ProductID}
để chỉnh sửa.", productId);
return NotFound();
}
return View(product);
}
// Action xử lý POST khi người dùng gửi form
[HttpPost]
[ValidateAntiForgeryToken] // Quan trọng cho bảo mật
public IActionResult Edit(Product product)
{
if (ModelState.IsValid) // Kiểm tra hợp lệ dựa trên Data
Annotations
{
// Logic lưu sản phẩm vào repository (chưa triển khai thực
sự)
// Ví dụ: _repository.SaveProduct(product);
_logger.LogInformation("Dữ liệu sản phẩm cho '{ProductName}' hợp lệ. Sẵn sàng để lưu.", product.Name);
TempData["message"] = $"{product.Name} đã được lưu thành công!";
return RedirectToAction("List"); // Hoặc Admin Index
}
else
{
// Có lỗi validation, hiển thị lại form với các thông báo lỗi
_logger.LogWarning("Dữ liệu sản phẩm cho '{ProductName}' không hợp lệ. Lỗi xác thực: {Errors}", product.Name, string.Join("; ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)));
return View(product); // Trả về cùng View với Model có lỗi
}
}
}