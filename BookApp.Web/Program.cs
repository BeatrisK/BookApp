using BookApp.Data;
using BookApp.Services.Data;
using BookApp.Services.Data.Interfaces;
using BookApp.Data.Models.Repository;
using BookApp.Data.Models.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

string connString = builder.Configuration
    .GetConnectionString("SQLServer");

builder.Services
    .AddDbContext<BookDbContext>(options =>
    {
        options.UseSqlServer(connString);
    });

builder.Services
    .AddDefaultIdentity<IdentityUser>(cfg =>
    {

    })
    .AddEntityFrameworkStores<BookDbContext>();


builder.Services.AddScoped(typeof(IRepository<,>), typeof(BaseRepository<,>));
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
