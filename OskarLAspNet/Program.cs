using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OskarLAspNet.Contexts;
using OskarLAspNet.Models.Identity;
using OskarLAspNet.Helpers.Repos;
using OskarLAspNet.Helpers.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("SQL")));


//Services
builder.Services.AddScoped<AddressService>();
builder.Services.AddScoped<AuthService>();




//Repos
builder.Services.AddScoped<AddressRepository>();
builder.Services.AddScoped<UserAddressRepository>();



//Konfigurerar hur login ska fungera
builder.Services.AddIdentity<AppUser, IdentityRole>(x =>
{
    x.SignIn.RequireConfirmedAccount = false;
    x.Password.RequiredLength = 8;
    x.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<DataContext>();

builder.Services.ConfigureApplicationCookie(x =>
{
    x.LoginPath = "/login";
    x.LogoutPath = "/"; ;
    x.AccessDeniedPath = "/YouShallNotPass";
});


var app = builder.Build();
app.UseHsts();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
