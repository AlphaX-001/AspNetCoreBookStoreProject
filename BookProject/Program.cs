

using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

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
