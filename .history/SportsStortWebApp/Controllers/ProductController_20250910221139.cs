// Controllers/ProductController.cs
using Microsoft.AspNetCore.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Models;
using System.Linq;
public class ProductController : Controller
{
private readonly IProductRepository _repository;
public ProductController(IProductRepository repository)
{
_repository = repository;
}