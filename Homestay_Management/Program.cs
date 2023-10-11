using Homestay_Management.Data;
using Homestay_Management.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<DataContext>();
builder.Services.AddControllersWithViews();


//Cấu hình dịch vụ quản lý Session vào ứng dụng
builder.Services.AddSession();


////Cấu hình dịch vụ Email
//var configuration = new ConfigurationBuilder()     //Tạo đối tượng để xây dựng cấu hình của ứng dụng 
//                  .SetBasePath(builder.Environment.ContentRootPath)//Đặt đường dẫn cho cơ sở tệp cấu hình
//                  .AddJsonFile("appsettings.json") //Thêm tệp appsettings.json vào cấu hình 
//                  .Build();                        //Tạo đối tượng IConfiguration từ cấu hình đã xây dựng
//builder.Services.AddOptions();                     //Thêm dịch vụ Options vào DI container
////Đọc phần cấu hình được đặt tên là "MailSettings" từ tệp appsettings.json
//var mailSettings = configuration.GetSection("MailSettings");
////Đăng ký cấu hình MailSettings trong DI container
//builder.Services.Configure<MailSettings>(mailSettings);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

//Map tới Home Default
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();


IdentityUser user;
IdentityDbContext _context;

app.Run();
