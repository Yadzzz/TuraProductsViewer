using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using DataAccess;
using TuraProductsViewer.Services;

var builder = WebApplication.CreateBuilder(args);
//builder.Logging.ClearProviders();
//builder.Logging.AddConsole();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddTransient<DataAccessManager>();
builder.Services.AddTransient<ProductSearchService>();
builder.Services.AddScoped<PDFCreatorService>();
builder.Services.AddTransient<APIService>();
builder.Services.AddTransient<ReadFileService>();

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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
