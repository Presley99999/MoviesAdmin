using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MoviesAdmin.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuth0WebAppAuthentication(options =>
{
    options.Domain = builder.Configuration["Auth0:Domain"];
    options.ClientId = builder.Configuration["Auth0:ClientId"];
    options.ClientSecret = builder.Configuration["Auth0:ClientSecret"];
});

builder.Services.Configure<OpenIdConnectOptions>(
    Auth0Constants.AuthenticationScheme,
    options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            NameClaimType = "name",
            RoleClaimType = "https://localhost:7294/roles"
        };
    });

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
