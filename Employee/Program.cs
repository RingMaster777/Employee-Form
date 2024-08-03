using Employee.Data;
using Serilog;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.DataProtection;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog

builder.Host.UseSerilog((context, services, configuration) => configuration
                        .WriteTo.Console()
                        .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day));


// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
));

// builder.Services.AddSingleton<IWebHostEnvironment>(sp => sp.GetRequiredService<IWebHostEnvironment>());

// Register Data Protection services
builder.Services.AddDataProtection();
builder.Services.AddSingleton<DataProtectionHelper>();

// Configure Data Protection
builder.Services.AddDataProtection()
        .PersistKeysToFileSystem(new DirectoryInfo(@"c:\keys")) // Optional: specify key storage location
        .SetDefaultKeyLifetime(TimeSpan.FromDays(14)); // Optional: set key lifetime


// for repository
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

// for service classes
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


// Register the custom logging middleware
app.UseMiddleware<LoggingMiddleware>();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
