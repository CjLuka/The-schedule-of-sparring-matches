using Aplication.Services.Interfaces;
using Aplication.Services.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistance.Data;
using Persistance.Repo.Interfaces;
using Persistance.Repo.Repositories;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Domain.Models.Domain;
using Aplication;
using Application.Services.Interfaces;
using Application.Services.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));


//builder.Services.AddDbContext<AuthDbContext>(options =>
//    options.UseSqlServer(
//        builder.Configuration.GetConnectionString("AuthDbConnectionString")));

//builder.Services.AddIdentity<IdentityUser, IdentityRole>()
//    .AddEntityFrameworkStores<AuthDbContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase= false;
    options.Password.RequireUppercase= false;
    options.Password.RequireNonAlphanumeric= false;
    options.Password.RequiredLength = 5;
    options.Password.RequiredUniqueChars = 0;
});

//DODANE PRZEZ WYŒWIETLANY B£¥D - InvalidOperationException: No service for type 'Microsoft.AspNetCore.Identity.SignInManager`1[Microsoft.AspNetCore.Identity.IdentityUser]' has been registered.
//POMOG£O I DZIA£A
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Login";
    options.AccessDeniedPath= "/AccessDenied";
});

// Add services to the container.
builder.Services.AddRazorPages();
//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie(option =>
//    {
//        option.LoginPath = "/Account/Login";
//        option.ExpireTimeSpan= TimeSpan.FromMinutes(20);
//    });
builder.Services.AddScoped<IClubRepository, ClubRepository>();
builder.Services.AddScoped<IGameClassRepository, GameClassRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBranchClubRepository, BranchClubRepository>();
builder.Services.AddScoped<IMatchRepository, MatchRepository>();
builder.Services.AddScoped<IFootballPitchRepository, FootballPitchRepository>();
builder.Services.AddScoped<IMatchRequestRepository, MatchRequestRepository>();
builder.Services.AddScoped<IAdressRepository, AddressRepository>();

builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IClubServices, ClubServices>();
builder.Services.AddScoped<IGameClassServices, GameClassServices>();
builder.Services.AddScoped<IBranchClubServices, BranchClubServices>();
builder.Services.AddScoped<IMatchServices, MatchServices>();
builder.Services.AddScoped<IFootballPitchServices, FootballPitchServices>();
builder.Services.AddScoped<IMatchRequestServices, MatchRequestServices>();
builder.Services.AddScoped<IAdressServices, AdressServices>();

builder.Services.addAplication(builder.Configuration);
//builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

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
