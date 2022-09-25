
using BookProject.Controllers.Methods;
using BookProject.Data;
using BookProject.Helpers;
using BookProject.Models;
using BookProject.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//The below services are to use Dependency injection
builder.Services.AddScoped<IAutoId, AutoId>();
builder.Services.AddScoped<ILanguageOperation, LanguageOperation>();
builder.Services.AddScoped<IBookOperation, BookOperation>();
builder.Services.AddScoped<IUserOperations, UserOperations>();
builder.Services.AddScoped<IAccountManager, AccountManager>();
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IUserClaimsPrincipalFactory<UsersOfApplication>, ApplicationUserClaimsPrincipalFactory>();

//In the following code, TestAlertConfig is added to the service container
//with Configure and bound to configuration(to use IOptions for fetching data from appsettings.json file):

builder.Services.Configure<TestAlertConfig>("MicrosoftBook", builder.Configuration.GetSection("Test"));
builder.Services.Configure<TestAlertConfig>("GoogleBook", builder.Configuration.GetSection("Test2"));

//Customizing the Password Complexity
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
    options.Password.RequireLowercase = false;
    options.Password.RequireDigit=false;
    options.Password.RequireUppercase = false;

});

builder.Services.AddDbContext<BookStoreContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<UsersOfApplication, IdentityRole>().
    AddEntityFrameworkStores<BookStoreContext>();


#if DEBUG
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

//Uncomment this below part to Disable Client side validations.

//builder.Services.AddRazorPages().AddRazorRuntimeCompilation().AddViewOptions(option =>
//{
//    option.HtmlHelperOptions.ClientValidationEnabled = false;
//});
#endif

//Redirection in Authorization Attribute
builder.Services.ConfigureApplicationCookie(config =>
{
    config.LoginPath = "/login";
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();


//app.UseStaticFiles(
//    new StaticFileOptions()
//    {
//        FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Newbee")),
//        RequestPath = "/Newbee"
//    });

app.UseRouting();
//To use Log in
app.UseAuthentication();

app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=User}/{action=Login}/{id?}"
//);

app.MapControllers();

//app.MapControllerRoute(
//    name: "AboutUs",
//    pattern: "about-us",
//    defaults: new {controller="home", action= "About" } );


//pattern: "bookapp/{controller=Home}/{action=Index}/{id?}");                /can add attribute like that on URL

app.Run();
