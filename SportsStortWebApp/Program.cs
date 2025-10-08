using SportsStore.Domain.Abstract;
using SportsStore.Domain.Concrete;
using SportsStoreWebApp.Settings;

var builder = WebApplication.CreateBuilder(args);

// Đăng ký DI cho IProductRepository
builder.Services.AddScoped<IProductRepository, FakeProductRepository>();

// Thêm các dịch vụ vào container.
builder.Services.AddControllersWithViews();
// builder.Logging.AddConsole();
// builder.Logging.AddDebug();
builder.Services.Configure<PagingSettings>(builder.Configuration.GetSection("PagingSettings"));
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
var app = builder.Build();

// Hiện thông tin debug để sửa lỗi (bật khi cần)
// Console.WriteLine("FakeProductRepository: " + typeof(SportsStoreWebApp.Concrete.FakeProductRepository).FullName);


// Cấu hình pipeline xử lý HTTP request.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // Giá trị HSTS mặc định là 30 ngày. Có thể thay đổi cho môi trường production, xem https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseMiddleware<SportsStoreWebApp.Middleware.RequestLoggerMiddleware>();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();//Quan trọng
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
name: "product_by_category",
pattern: "san-pham/danh-muc/{category}",
defaults: new { controller = "Product", action = "ListByCategory" }
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapControllerRoute(
name: "default",
pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
