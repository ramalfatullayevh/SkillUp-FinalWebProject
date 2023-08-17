using Microsoft.AspNetCore.Identity;
using SkillUp.DAL.Context;
using SkillUp.DAL.Extension;
using SkillUp.Entity.Entities;
using SkillUp.Service.Extensions;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

builder.Services.DataLayerExtension(builder.Configuration);

builder.Services.LoadServiceLayerExtension();

builder.Services.AddControllersWithViews();

StripeConfiguration.ApiKey = builder.Configuration.GetValue<string>("Stripe:SecretKey");

builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
{
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequireDigit = true;
    opt.Password.RequiredLength = 6;
    opt.Lockout.AllowedForNewUsers = true;
    opt.User.RequireUniqueEmail = true;
    opt.SignIn.RequireConfirmedEmail = false;
}).AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddIdentityCore<Instructor>(opt =>
{
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequireDigit = true;
    opt.Password.RequiredLength = 6;
    opt.Lockout.AllowedForNewUsers = true;
}).AddDefaultTokenProviders().AddRoles<IdentityRole>().AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseStatusCodePagesWithReExecute("/Error/NotFound", "?code={0}");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
          );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();