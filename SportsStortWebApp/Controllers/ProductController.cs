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
    public IActionResult Edit([FromForm] Product product)
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
    // Nhận category và productPage từ QueryString hoặc Route data
        public IActionResult List([FromQuery] string? category = null, [FromQuery(Name = "productPage")] int productPage = 1, [FromQuery(Name = "itemsPerPage")] int itemsPerPage = 3)
        {
            // các biến phân trang đơn giản (có thể cải thiện bằng dịch vụ phân trang)
            if (itemsPerPage < 1) itemsPerPage = 3; // safe default

            // Chuẩn hóa category: coi chuỗi rỗng hoặc whitespace như không có category (null)
            if (string.IsNullOrWhiteSpace(category))
            {
                category = null;
            }

            // Đảm bảo productPage hợp lệ
            if (productPage < 1) productPage = 1;

            var filtered = _repository.Products.Where(p => category == null || p.Category == category);
            var products = filtered.Skip((productPage - 1) * itemsPerPage).Take(itemsPerPage).ToList();

            ViewBag.CurrentCategory = category ?? "Tất cả sản phẩm";
            ViewBag.Message = TempData["message"];
            ViewBag.CurrentPage = productPage;
            ViewBag.ItemsPerPage = itemsPerPage;
            ViewBag.TotalItems = filtered.Count();

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
        
    // GET: Product/Filter - bind từ query string
        [HttpGet]
        public IActionResult FilterProducts([FromQuery] ProductFilter filter)
        {
            _logger.LogInformation("Lọc sản phẩm theo Category: {Category}, MinPrice: {MinPrice}, MaxPrice: {MaxPrice}, InStockOnly: {InStock}",
                filter.Category, filter.MinPrice, filter.MaxPrice, filter.InStockOnly);

            var results = ApplyFilter(filter);
            return View("List", results);
        }

    // POST: Product/Filter - bind từ form
    // Đổi tên phương thức để tránh trùng chữ ký nhưng vẫn giữ tên action cho routing/forms
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("FilterProducts")]
        public IActionResult FilterProductsPost([FromForm] ProductFilter filter)
        {
            // Reuse the same helper used by the GET action
            var results = ApplyFilter(filter);
            return View("List", results);
        }

    // Hàm helper dùng chung để lọc sản phẩm
        private List<Product> ApplyFilter(ProductFilter filter)
        {
            var filteredProducts = _repository.Products.AsQueryable();
            if (!string.IsNullOrEmpty(filter.Category))
            {
                filteredProducts = filteredProducts.Where(p => p.Category == filter.Category);
            }
            if (filter.MinPrice.HasValue)
            {
                filteredProducts = filteredProducts.Where(p => p.Price >= filter.MinPrice.Value);
            }
            if (filter.MaxPrice.HasValue)
            {
                filteredProducts = filteredProducts.Where(p => p.Price <= filter.MaxPrice.Value);
            }
            if (filter.InStockOnly)
            {
                // Nếu model Product có thuộc tính InStock, có thể lọc tại đây.
                // Ví dụ: filteredProducts = filteredProducts.Where(p => p.InStock > 0);
            }

            return filteredProducts.ToList();
        }
         [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int productId)
        {
            var productToDelete = _repository.Products.FirstOrDefault(p => p.ProductID == productId);
            if (productToDelete != null)
            {
                // gọi repository để xóa
                _repository.DeleteProduct(productId);
                TempData["message"] = $"{productToDelete.Name} đã được xóa!";
            }
            else
            {
                TempData["message"] = $"Sản phẩm có ID {productId} không tồn tại để xóa!";
            }
            return RedirectToAction("List");
        }
    }
}