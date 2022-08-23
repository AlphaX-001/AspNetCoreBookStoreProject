
using BookProject.Controllers.Methods;
using BookProject.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//The below services are to use Dependency injection
builder.Services.AddScoped<IAutoId, AutoId>();
builder.Services.AddScoped<ILanguageOperation, LanguageOperation>();
builder.Services.AddScoped<IBookOperation, BookOperation>();
builder.Services.AddScoped<IUserOperations, UserOperations>();

//In the following code, TestAlertConfig is added to the service container
//with Configure and bound to configuration(to use IOptions for fetching data from appsettings.json file):

builder.Services.Configure<TestAlertConfig>("MicrosoftBook", builder.Configuration.GetSection("Test"));
builder.Services.Configure<TestAlertConfig>("GoogleBook", builder.Configuration.GetSection("Test2"));

//Chganging password complexity

//builder.Services.Configure<NewUserModel>(options =>
//{
//    options.password.
//    });


#if DEBUG
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

//Uncomment this below part to Disable Client side validations.

//builder.Services.AddRazorPages().AddRazorRuntimeCompilation().AddViewOptions(option =>
//{
//    option.HtmlHelperOptions.ClientValidationEnabled = false;
//});
#endif

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

app.UseAuthorization();

//To use Log in
app.UseAuthentication();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllers();

//app.MapControllerRoute(
//    name: "AboutUs",
//    pattern: "about-us",
//    defaults: new {controller="home", action= "About" } );


//pattern: "bookapp/{controller=Home}/{action=Index}/{id?}");                /can add attribute like that on URL

app.Run();
