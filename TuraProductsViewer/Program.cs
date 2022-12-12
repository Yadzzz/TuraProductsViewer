using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using Serilog;
using System.Net;
using System.Runtime.CompilerServices;
using TuraProductsViewer;
using TuraProductsViewer.Pages;
using TuraProductsViewer.Services;
using TuraProductsViewer.Services.FileCreator;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

//DI
builder.Services.AddScoped<CreatorService>();
builder.Services.AddTransient<APIService>();
builder.Services.AddTransient<ReadFileService>();
builder.Services.AddScoped<ImageService>();
builder.Services.AddTransient<PdfCreatorService>();
builder.Services.AddTransient<NavisionDataRetriever.NavisionRetriever>();
builder.Services.AddTransient<DataRetriever.API.ProductsData>();
builder.Services.AddTransient<ConcreteExcelCreator>();
builder.Services.AddTransient<NotificationService>();

builder.Services.AddHsts(options =>
{
    options.Preload = true;
    options.IncludeSubDomains = true;
    options.MaxAge = TimeSpan.FromDays(365);
});

Log.Logger = new LoggerConfiguration()
.Enrich.FromLogContext()
.WriteTo.File(@"logs/logs.txt", shared: true)
.CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog();

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

//string networkPath = @"\\192.168.1.21\Produktbilder";
//NetworkCredential credentials = new NetworkCredential(@"tura\svc-pdf", "1234QWer!");

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
