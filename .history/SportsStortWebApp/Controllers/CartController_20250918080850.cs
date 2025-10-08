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
        
}