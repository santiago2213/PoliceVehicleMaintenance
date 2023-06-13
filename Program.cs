using DiscussionLibrarySantiago;
using DiscussionMvcSantiago.Data;
using DiscussionMvcSantiago.Models;
using DiscussionMvcSantiago.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<AppUser>
                (options =>
                {
                    options.SignIn.RequireConfirmedAccount = true;
                    options.Password.RequiredLength = 6;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireDigit = true;
                    options.User.RequireUniqueEmail = true;
                }
                ) //change to true later
                  //Need to add RoleManager reference here
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IVehicleRepo, VehicleRepo>();
builder.Services.AddTransient<IAppUserRepo, AppUserRepo>();
builder.Services.AddTransient<IServiceRequestRepo, ServiceRequestRepo>();
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddTransient<IServiceRequestEmailSender, ServiceRequestEmailSender>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    //3 services: database, app users, roles
    var services = scope.ServiceProvider;

    try
    {
        InitialDatabase.SeedDatabase(services);
    }
    catch(Exception serviceException)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(serviceException, "Error occured while using the db, user, or role service");
    }

}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
