using Lab02.Models;
using Lab02.Repository;
using Lab02.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AddbContext>();

builder.Services.AddScoped<IProductRepository, ProductService>();
builder.Services.AddScoped<ICustomerRepository, CustomerService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession();

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

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Customer}/{action=Login}/{id?}");

app.Run();
