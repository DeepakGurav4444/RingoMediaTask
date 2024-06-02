using Microsoft.EntityFrameworkCore;
using RingoMediaTask.Data;
using RingoMediaTask.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//builder.Services.AddDbContext<AdminDbContext>((options)=>options.UseMySQL(builder.Configuration.GetConnectionString("AdminPortal") ??""));

builder.Services.AddDbContextPool<AdminDbContext>(options =>
{
    options.UseMySQL(builder.Configuration.GetConnectionString("AdminPortal") ?? "");
}, 500);
var serviceRegistry = new ServiceRegistry();
serviceRegistry.ConfigureDependencies(builder.Services);
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
