using Microsoft.AspNetCore.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Models;
using SportsStoreWebApp.Extensions;

public class CartController : Controller
{
    private readonly IProductRepository _repository;
    private readonly ILogger<CartController> _logger;
    public CartController(IProductRepository repo, ILogger<CartController> logger)
    {
        _repository = repo;
        _logger = logger;
    }
    public IActionResult AddToCart(int productId, string returnUrl)
    {
        Product? product = _repository.Products.FirstOrDefault(p => p.ProductID == productId);
        if (product != null)
        {
            Cart cart =HttpContext.Session.GetObjectFromJson<Cart>("Cart") ?? new Cart();
            cart.AddItem(product, 1); // Thêm 1 sản phẩm
            HttpContext.Session.SetObjectAsJson("Cart", cart); // Lưu lại giỏ hàng vào Session
            _logger.LogInformation("Added product {ProductName} (ID:{ProductID}) to cart. Total items in cart: {TotalItems}", product.Name, product.ProductID, cart.Lines.Sum(i => i.Quantity));
        }
        esle 
        {
            _logger.LogWarning("Attempted to add non-existent product
with ID {ProductID} to cart.", productId);
        }
}