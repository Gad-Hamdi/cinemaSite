using cinemaSite.DataAccess;
using cinemaSite.Models;
using cinemaSite.Repositories;
using cinemaSite.Repositories.IRepositories;
using cinemaSite.Utitlity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Stripe;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        // Register ApplicationDbContext
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


        builder.Services.AddIdentity<ApplicationUser, IdentityRole>(option =>
        {
            option.Password.RequireNonAlphanumeric = false;
            option.Password.RequiredLength = 8;
            option.User.RequireUniqueEmail = true;
        });
                
        
        builder.Services.ConfigureApplicationCookie(option =>
        {
            option.LoginPath = "/Identity/Account/Login";
            option.AccessDeniedPath = "/Customer/Home/NotFoundPage";
        });

        builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));
        StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];

        // Register generic repository
        builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}