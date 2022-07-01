

using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#if DEBUG
    builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
    
    //pattern: "bookapp/{controller=Home}/{action=Index}/{id?}");                /can add aattribute like that on URL

app.Run();
