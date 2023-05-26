using Aplication.Services.Interfaces;
using Aplication.Services.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistance.Data;
using Persistance.Repo.Interfaces;
using Persistance.Repo.Repositories;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));




// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.LoginPath = "/Account/Login";
        option.ExpireTimeSpan= TimeSpan.FromMinutes(20);
    });
builder.Services.AddScoped<IClubRepository, ClubRepository>();
builder.Services.AddScoped<IGameClassRepository, GameClassRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBranchClubRepository, BranchClubRepository>();
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IClubServices, ClubServices>();
builder.Services.AddScoped<IGameClassServices, GameClassServices>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
//});
builder.Services.AddAuthorization();
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

app.UseAuthentication();

app.UseAuthorization();


app.MapRazorPages();

app.Run();
