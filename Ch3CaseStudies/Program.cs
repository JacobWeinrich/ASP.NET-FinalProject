using Ch3CaseStudies.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<SportsProContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("SportsProContext")));

builder.Services.AddRouting(options => { options.LowercaseUrls = true; options.AppendTrailingSlash = true; });

builder.Services.AddMemoryCache();
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
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}/{slug?}");

app.MapControllerRoute(
    name: "TechIncident",
    pattern: "{controller=TechIncident}/{action=List}/{id?}/{slug?}");


app.Run();
