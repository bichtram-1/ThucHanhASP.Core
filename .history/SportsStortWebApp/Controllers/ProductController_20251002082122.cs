using Microsoft.AspNetCore.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Models;
using SportsStore.Domain.Entities;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace SportsStoreWebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _repository;
        private readonly ILogger<ProductController> _logger;
        
        public ProductController(IProductRepository repository, ILogger<ProductController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        // GET: Product/Edit/5 hoặc Product/Edit (tạo mới)
        public IActionResult Edit(int id = 0)
        {
            Product? product = id == 0 ? new Product() : 
                _repository.Products.FirstOrDefault(p => p.ProductID == id);
                
            if (product == null && id != 0)
            {
                _logger.LogWarning("Không tìm thấy sản phẩm có ID {ProductID} để chỉnh sửa.", id);
                return NotFound();
            }
            
            return View(product);
        }

        // POST: Product/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _repository.SaveProduct(product);
                _logger.LogInformation("Dữ liệu sản phẩm cho '{ProductName}' hợp lệ. Đã lưu thành công.", product.Name);
                TempData["message"] = $"{product.Name} đã được lưu thành công!";
                return RedirectToAction("List");
            }
            else
            {
                _logger.LogWarning("Dữ liệu sản phẩm cho '{ProductName}' không hợp lệ.", product.Name);
                return View(product);
            }
        }

        // GET: Product/List
        public IActionResult List(string? category = null)
        {
            var products = _repository.Products.Where(p => category == null || p.Category == category).ToList();
            ViewBag.CurrentCategory = category ?? "Tất cả sản phẩm";
            ViewBag.Message = TempData["message"];
            return View(products);
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            return View(new Product());
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                // Tạo ID ngẫu nhiên cho sản phẩm mới (trong thực tế sẽ do database tự tạo)
                product.ProductID = new Random().Next(1000, 9999);
                _repository.SaveProduct(product);
                _logger.LogInformation("Sản phẩm {ProductName} (ID:{ProductID}) đã được tạo thành công.", product.Name, product.ProductID);
                TempData["message"] = $"Sản phẩm '{product.Name}' đã được tạo thành công với ID: {product.ProductID}";
                return RedirectToAction("List");
            }
            
            _logger.LogWarning("Tạo sản phẩm thất bại do trạng thái model không hợp lệ cho sản phẩm: {ProductName}", product.Name);
            return View(product);
        }

        // GET: Product/Details/5
        [Route("chi-tiet/{id:int}")]
        public IActionResult Details(int id)
        {
            var product = _repository.Products.FirstOrDefault(p => p.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
    }
     [HttpPost]
        public IActionResult Delete(int productId)
        {
            Product? productToDelete=_repository.Products.FirstOrDefault(p => p.ProductID==productId);
            if(productToDelete!=null)
            {
                TempData["message"]=$"{productToDelete.Name} Đã được xóa!";
            }
            else
        }
}