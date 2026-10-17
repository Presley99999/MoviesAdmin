using Microsoft.EntityFrameworkCore;
using MoviesAdmin.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("MoviesAdminContext")
    ?? throw new InvalidOperationException("Connection string 'MoviesAdminContext' not found.");

builder.Services.AddDbContext<MoviesAdminContext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.AddControllersWithViews();


builder.Services.AddSingleton<IAzureStorageService, AzureStorageService>();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
