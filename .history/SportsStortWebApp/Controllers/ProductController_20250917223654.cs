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

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SportsStore.Domain.Models;
using System;
public class ProductController: Controller
{
    private readonly ILogger<ProductController> _logger;
    public ProductController(ILogger<ProductController> logger)
    { 
        _logger=logger;

    }
    public IActionResult Create(){
        return View();
    }
    USERS (NGƯỜI DÙNG)
+ UserID: int  	
+ UserName: nvarchar(50)
+ Password: nvarchar(255)
+ Email: nvarchar(100)
+ Phone: nvarchar(10)
+ Birth: datetime
+ Sex: nvarchar(4)
+ Address: nvarchar(100)
+ IsActive: bit 	//0 – Không hoạt động, 1 – Hoạt động
NEWLETTERS (BẢN TIN)
+ Email: nvarchar(100)
+ SubscribedAt: datetime DEFAULT GETDATE()
INFORMATION (THÔNG TIN SHOP)
+ InfoID: int
+ Name: nvarchar(100)
+ Facebook: nvarchar(200)
+ Twitter: nvarchar(200)
+ Youtube: nvarchar(200)
+ Linkedln: nvarchar(200)
+ Pinterest: nvarchar(200)
ARTICLES
+ ArticleID: int
+ Title: nvarchar(max) 			//Phần trang tin tức ở giao diện home
+ Description: nvarchar(max) 		//Mô tả ngắn
+ Content: nvarchar(max)			//Nội dung chi tiết 
+ Status: int 					//0 – Nháp, 1 – Đã đăng
PRODUCTS (SẢN PHẨM)
+ ProductID: int
+ Name: nvarchar(200)
+ Quantity: int
+ ImportPrice: decimal(10,2)	//Giá nhập,  decimal(18, 3) là tối đa 18 chữ số, lưu 3 số thập phân sau dấu phẩy
+ SellingPrice: decimal(10,2)		//Giá bán
+ DiscountedPrice: decimal(5,2)		//Giá sau giảm
+ Tag: nvarchar(255)
+ Color: nvarchar(20)
+ Decription: nvarchar(max)
+ Status: int 				//0 – Xóa, 1 – Hết hàng, 2 – Còn hàng
+ CategoryID: int
+ VendorID: int
IMAGES_PRODUCT (HÌNH ẢNH CỦA SẢN PHẨM)
+ ProductID : int
+ ImageID: nvarchar(255)
REVIEWS (BÌNH LUẬN)
// chỉ khách hàng đã mua mới được bình luận, chưa mua vẫn dc xem bình luận
+ ReviewID: int
+ Rating: int check (rating between 1 and 5)	//Số sao
+ ReviewsText: nvarchar(max)
+ PostAt: datetime 				//Ngày giờ bình luận
+ UserID: int
+ ProductID: int
CARTS
+ CartID: int
+ Status: int 				//0 – Đã xóa, 1 – Đã thanh toán, 2 – Hoạt động
+ UserID: int
CARTITEMS
+ CartID: int
+ ProductID: int
+ Price: decimal(10,2)
+ Quantity: int check (quantity > 0)
+ total: decimal(10,2)		//Tổng tiền 1 sản phẩm trong giỏ
COUPONS
+ CouponID: int
+ Code: char(20) UNIQUE
+ Discount: decimal(10,2)	//Phần trăm giảm hoặc số tiền giảm
+ CreateAt: datetime	//Ngày tạo
+ ExpiredAt: datetime  	//Ngày hết hạn
+ Status: int			//0 – Hết hạn/hết số lượng, 1 – Còn hạn/còn số lượng
+ Quantity: int		
+ MinOrderValue: decimal(10, 2) 	 	//Giá trị đơn hàng tối thiểu
+ MaxDiscount: decaimal(10, 2)		//Số tiền giảm tối đa	
+ Type: int			//0 – Giảm theo %, 1 – Giảm số tiền 
+ Target: int			//0 – AD cho đơn hàng, 1 – AD ship
CATEGORY
+ CategoryID: int
+ Name: nvarchar(255)
+ Description: nvarchar(max)
+ IsActive: bit			// 0 – Hoạt động, 1 – Không hoạt động
VENDORS
+ VendorID: int 
+ Name: nvarchar(255)
+ Email: nvarchar(100)
+ Phone: nvarchar(10)
+ Address: nvarchar(255)
+ IsActive: bit			// 0 – Hoạt động, 1 – Không hoạt động
CONTACT
+ ContactID: int
+ Name: nvarchar(255)
+ Phone: nvarchar(10)
+ Email: nvarchar(100)
+ Message: nvarchar(max)
+ CreateAt: datetime
+ Status: int 			// 0 – Chưa xử lý, 1 – Đã xử lý
PAYMENT
+ PaymentID: int
+ PaymentMethod: nvarchar(100)
+ IsActive: bit			// 0 – Không hoạt động, 1 – Hoạt động
ORDERS
+ OrderID: int
+ OrderDate: datetime
+ ShippingFee: decimal(10,2)
// TotalAmount: decimal(10,2) Tổng tiền sau giảm có thể tính toán bằng công thức
+ Sumtotal: decimal(10,2)		//Tổng tiền
+ Note: nvarchar(max)
+ Status: int 			// 0 – Chờ xử lý, 1 – Đang giao, 2 – Hoàn tất, 3 – Hủy
+ Discount: decimal(10,2) 	// Số tiền giảm giá
+ ShippingAddress: varchar(255)
+ PaymentID: int
+UserID: int
+ CouponID: int
ORDER_DETAILS
+ OrderID: int
+ ProductID: int
+ Quantity: int
+ Price: decimal(10,2)
+ Subtotal: decimal(10,2) = Price * quantity		//Tổng giá của từng sản phẩm

    public IActionResult Create (Product NewProduct) 
    {
        _logger.LogInformation("Đang cố gắng tạo sản phẩm mới: {ProductName}",NewProduct.Name);
        if(ModelState.IsValid)
        {
            newProduct.ProductID=new Random().Next(1000,9999);
            _logger.LogInformation("Sản phẩm {ProductName} (ID:{ProductID} đã được tạo thành công.", newProduct.Name,newProduct.ProductID);
            TemData["SuccessMessage"]=$"sản phẩm"
        }
    }

}
//bx sau học quản lý trạng thái
//4 cách truyền từ controller sang view và ngược lại
//controller ko muốn xuất ra view thì dùng gì để hiện
