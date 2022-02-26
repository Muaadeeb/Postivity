using Blazor_Server.Data;
using Business.Repository;
using Business.Repository.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
//using System.Configuration;
using Microsoft.Extensions.Configuration;
using DataAccess.Data;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();


// Add NuGet - Microsoft.EntityFrameworkCore.SqlServer, Microsoft.EntityFrameworkCore.Design
// Tools.NuGetPackageManager.PackageManagerConsole:
// Enter Command: add-migration CreateDatabase [press enter]  
// ((Be sure in the PMC that the "Default project:" is set to DataAccess (in the same windwo as PMC).
// then run command: "update-database"


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
 
builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

// When we added scaffolding; it did not know about our ApplicationDbContext configuration, so it added it again.
// We just need to detele the following line.
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(connectionString));


//TODO: THis now breaks after we did update for NAME - c.f. API project startup - ApplicationUser.
//services.AddIdentity<IdentityUser, IdentityRole>()
//    .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders()
//    .AddDefaultUI();

//services.AddScoped<IDbInitializer, DbInitializer>();

//services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//builder.Services.AddScoped<IFriendsRepository, FriendsRepository>();
builder.Services.AddScoped<IUserRepository, Business.Repository.UserRepository>();
//builder.Services.AddScoped<IFriends, Business.Repository.Friend>();
//builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddDefaultTokenProviders().AddDefaultUI()
//    .AddEntityFrameworkStores<ApplicationDbContext>();
//builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
//builder.Services.AddScoped<IOrderRepository, OrderRepository>();
//builder.Services.AddScoped<IProductRepository, ProductRepository>();
//builder.Services.AddScoped<IDbInitializer, DbInitializer>();
//builder.Services.AddScoped<IProductPriceRepository, ProductPriceRepository>();
//builder.Services.AddScoped<IFileUpload, FileUpload>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


builder.Services.AddHttpContextAccessor();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
