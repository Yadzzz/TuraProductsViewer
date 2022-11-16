using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.FileProviders;
using System.Net;
using TuraProductsViewer;
using TuraProductsViewer.Services;

var builder = WebApplication.CreateBuilder(args);
//builder.Logging.ClearProviders();
//builder.Logging.AddConsole();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<CreatorService>();
builder.Services.AddTransient<APIService>();
builder.Services.AddTransient<ReadFileService>();
builder.Services.AddScoped<FileCreatorService>();
builder.Services.AddScoped<ImageService>();

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

string networkPath = @"\\192.168.1.21\Produktbilder";
NetworkCredential credentials = new NetworkCredential(@"tura\svc-pdf", "1234QWer!");

//ConnectToSharedFolder ConnectToSharedFolder = new ConnectToSharedFolder(networkPath, credentials);

//app.UseFileServer(new FileServerOptions()
//{
//    FileProvider = new PhysicalFileProvider(
//        Path.Combine("\\\\192.168.1.21\\Produktbilder")),
//    RequestPath = new PathString("/Produktbilder"),
//    EnableDirectoryBrowsing = true
//});

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
