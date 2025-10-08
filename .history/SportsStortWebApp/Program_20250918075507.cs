using SportsStore.Domain.Abstract;
using SportsStoreWebApp.Configurations;
using SportsStoreWebApp.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Đăng ký DI cho IProductRepository
builder.Services.AddScoped<IProductRepository, FakeProductRepository>();

// Add services to the container.
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

//Hiện lỗi để sửa:
// Console.WriteLine("FakeProductRepository: " + typeof(SportsStoreWebApp.Concrete.FakeProductRepository).FullName);


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
