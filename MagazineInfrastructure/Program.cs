using MagazineDomain.Model;
using MagazineInfrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MagazineInfrastructure.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MagazineInfrastructureContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("IstpContext") ?? throw new InvalidOperationException("Connection string 'MagazineInfrastructureContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure the database context
builder.Services.AddDbContext<IstpContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add authorization services
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // Detailed error page for development
}
else
{
    app.UseExceptionHandler("/Home/Error"); // Custom error handling page for production
    app.UseHsts(); // Add HTTP Strict Transport Security (HSTS)
}

app.UseHttpsRedirection(); // Redirect HTTP requests to HTTPS
app.UseStaticFiles(); // Serve static files from wwwroot

app.UseRouting(); // Enable routing

app.UseAuthorization(); // Enable authorization

// Configure default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Authors}/{action=Index}/{id?}");

app.Run(); // Run the application
