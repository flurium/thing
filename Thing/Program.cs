using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Thing.Context;
using Thing.Infrastructure;
using Thing.Models;
using Thing.Repository;
using Thing.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var aspEnv = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
string connectionString;

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

// SEND GRID
builder.Services.AddTransient<IEmailSender, EmailSenderService>();

// Repositories
builder.Services.AddScoped<AnswerRepository>();
builder.Services.AddScoped<CategoryRepository>();
builder.Services.AddScoped<CommentImageRepository>();
builder.Services.AddScoped<CommentRepository>();
builder.Services.AddScoped<FavoriteRepository>();
builder.Services.AddScoped<OrderRepository>();
builder.Services.AddScoped<ProductImageRepository>();
builder.Services.AddScoped<ProductRepository>();
builder.Services.AddScoped<PropertyValueRepository>();
builder.Services.AddScoped<RequiredPropertyRepository>();
builder.Services.AddScoped<SellerRepository>();

// Logic services
builder.Services.AddTransient<CategoryService>();
builder.Services.AddTransient<BanService>();
builder.Services.AddTransient<RequiredPropertiesService>();
builder.Services.AddTransient<ProductService>();
builder.Services.AddTransient<SellerService>();

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