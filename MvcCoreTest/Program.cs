using Microsoft.EntityFrameworkCore;
using MvcCoreTest.Context;
using MvcCoreTest.Services;
using MvcCoreTest.Services.Base;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<DbContext, ArabaContext>();
builder.Services.AddScoped<IArabaServis, ArabaServis>();
builder.Services.AddScoped<IAciklamaServis, AciklamaServis>();
builder.Services.AddScoped<IUreticiServis, UreticiServis>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
