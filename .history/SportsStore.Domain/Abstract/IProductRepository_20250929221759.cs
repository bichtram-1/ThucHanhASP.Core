// // SportsStore.Domain/Abstract/IProductRepository.cs
// using SportsStore.Domain.Models;
// namespace SportsStore.Domain.Abstract
// {
// public interface IProductRepository
// {
// // Thuộc tính để lấy tất cả sản phẩm
// IQueryable<Product> Products { get; }
// }
// }

using System.Linq;
using SportsStore.Domain.Entities;

namespace SportsStore.Domain.Abstract
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }
        void SaveProduct(Product product);
    }
}
