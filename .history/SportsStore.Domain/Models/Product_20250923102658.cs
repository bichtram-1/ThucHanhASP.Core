// using System.ComponentModel.DataAnnotations; // Để dùng [Key]
// namespace SportsStore.Domain.Models
// {
// // Class Product đại diện cho một sản phẩm trong cửa hàng
// public class Product
// {
// [Key] // Đánh dấu đây là khóa chính
// public int ProductID { get; set; }
// public string Name { get; set; } = string.Empty;
// public string Description { get; set; } = string.Empty;
// public decimal Price { get; set; }
// public string Category { get; set; } = string.Empty;

// }
// }
//view viết bằng blade bên laravel, còn bên core sài razer


using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SportsStore.Domain.Models
{
// Class Product đại diện cho một sản phẩm trong cửa hàng
// public class Product
// {
// [Key] // Đánh dấu đây là khóa chính
// public int ProductID { get; set; }
// [Required(ErrorMessage = "Tên sản phẩm là bắt buộc.")]
// [StringLength(100, MinimumLength = 3, ErrorMessage = "Tên sản phẩm phải có từ 3 đến 100 ký tự.")]
// public string Name { get; set; } = string.Empty;
// public string Description { get; set; } = string.Empty;
// [Range(0.01, double.MaxValue, ErrorMessage = "Giá phải lớn hơn 0.")]
// public decimal Price { get; set; }
// public string Category { get; set; } = string.Empty;
// }
[Table("Products")] // Ánh xạ lớp Product tới bảng "Products"
public class Product
{
[Key]
[Display(Name = "Mã SP")]
public int ProductID { get; set; }
[Required(ErrorMessage = "Vui lòng nhập tên sản phẩm.")]
[StringLength(100, MinimumLength = 3, ErrorMessage = "Tên sản phẩm phải từ 3 đến 100 ký tự.")]
[Display(Name = "Tên sản phẩm")]
public string Name { get; set; } = string.Empty;
[Required(ErrorMessage = "Vui lòng nhập mô tả.")]
[StringLength(500, ErrorMessage = "Mô tả không được vượt quá 500 ký tự.")]
[DataType(DataType.MultilineText)]
[Display(Name = "Mô tả")]
public string Description { get; set; } = string.Empty;
[Required(ErrorMessage = "Vui lòng nhập giá.")]

}