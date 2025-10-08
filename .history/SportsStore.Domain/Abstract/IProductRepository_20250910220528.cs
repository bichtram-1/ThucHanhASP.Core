// SportsStore.Domain/Abstract/IProductRepository.cs
using SportsStore.Domain.Models;
namespace SportsStore.Domain.Abstract
{
public interface IProductRepository
{
// Thuộc tính để lấy tất cả sản phẩm
IQueryable<Product> Products { get; }
}
}