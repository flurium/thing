using Bll.Infrastructure;
using Dal.Context;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

string connectionString;
var aspEnv = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

if (aspEnv == "Development")
{
  connectionString = builder.Configuration.GetConnectionString("SupabasePostgres");
  builder.Services.Configure<SendGridOptions>(options => builder.Configuration.GetSection("SendGridOptions").Bind(options));
}
else
{
  /* Should be tested!
   * We will use enviroment variables, because:
   * It's free (not as Azure Secrets)
   * Secure (not showed in repo)
   */
  connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STR") ?? "";
  builder.Services.Configure<SendGridOptions>(options =>
  {
    options.UserMail = Environment.GetEnvironmentVariable("SG_USER_MAIL") ?? "";
    options.SendGridKey = Environment.GetEnvironmentVariable("SG_API_KEY") ?? "";
  });
}

// DB
builder.Services.AddDbContext<ThingDbContext>(options => options.UseNpgsql(connectionString)); // (connectionString, new MySqlServerVersion(new Version(8, 0, 31))));

// AUTH
builder.Services.AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedEmail = true)
   .AddEntityFrameworkStores<ThingDbContext>().AddDefaultTokenProviders().AddTokenProvider<EmailConfirmationTokenProvider<User>>("emailConfirmationProvider");

builder.Services.Configure<EmailConfirmationProviderOptions>(options => options.TokenLifespan = TimeSpan.FromDays(1));

BllConfiguration.ConfigureServices(builder.Services);

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "emailConfirmation",
    pattern: "confirmation/",
    defaults: new { controller = "EmailConfirm", action = "Confirm" });

app.Run();