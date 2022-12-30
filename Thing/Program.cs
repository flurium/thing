using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Thing.Context;
using Thing.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// DB
var connectionString = builder.Configuration.GetConnectionString("Local");
builder.Services.AddDbContext<ThingDbContext>(options => options.UseSqlServer(connectionString));

// AUTH
builder.Services.AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedEmail = true)
   .AddEntityFrameworkStores<ThingDbContext>();

//builder.Services.Configure<EmailConfirmationProviderOptions>(options => options.TokenLifespan = TimeSpan.FromDays(1));

//// SEND GRID
//builder.Services.Configure<SendGridOptions>(options => builder.Configuration.GetSection("SendGridOptions").Bind(options));
//builder.Services.AddTransient<IEmailSender, EmailSenderService>();

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

//app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();