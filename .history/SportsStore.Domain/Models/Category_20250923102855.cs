using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic; // Để dùng List
namespace SportsStore.Domain.Models
{
[Table("Categories")]
public class Category
{
[Key]
[Display(Name = "Mã danh mục")]
public int CategoryID { get; set; }
[Required(ErrorMessage = "Vui lòng nhập tên danh mục.")]
[StringLength(50, MinimumLength = 3, ErrorMessage = "Tên danh mục
phải từ 3 đến 50 ký tự.")]
[Display(Name = "Tên danh mục")]
public string Name { get; set; } = string.Empty;
// Navigation property (nếu có EF Core)
// public virtual ICollection<Product>? Products { get; set; }
}
}