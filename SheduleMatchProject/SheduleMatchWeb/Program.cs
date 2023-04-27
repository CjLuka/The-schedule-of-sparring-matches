using Aplication.Services.Interfaces;
using Aplication.Services.Services;
using Microsoft.EntityFrameworkCore;
using Persistance.Data;
using Persistance.Repo.Interfaces;
using Persistance.Repo.Repositories;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddScoped<IClubRepository, ClubRepository>();
builder.Services.AddScoped<IGameClassRepository, GameClassRepository>();
builder.Services.AddScoped<IClubServices, ClubServices>();
builder.Services.AddScoped<IGameClassServices, GameClassServices>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
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

app.UseAuthorization();

app.MapRazorPages();

app.Run();
