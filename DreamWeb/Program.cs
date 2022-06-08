using DreamWeb;
using DreamWeb.DAL;
using DreamWeb.DAL.Entities;
using DreamWeb.DreamPublicationSorting;
using DreamWeb.Models;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc();
builder.Services.AddControllersWithViews(); 
builder.Services.AddScoped<DreamInputModel, DreamInputModel>();

if (!builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<DreamsContext>(options =>
        options.UseMySql(builder.Configuration.GetConnectionString("Database"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("Database"))));
    builder.Services.Configure<ForwardedHeadersOptions>(options =>
    {
        options.KnownProxies.Add(IPAddress.Parse("10.110.0.2"));
    });
}
else
{
    builder.Services.AddDbContext<DreamsContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("Database")));
}

builder.Services.AddDefaultIdentity<UserAccount>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<DreamsContext>();
builder.Services.AddTransient<IDatabaseReader, DatabaseReaderSQL>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddTransient<IDreamsSorting, DreamsSorting>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddAuthentication().AddCookie();


builder.Services.Configure<IdentityOptions>(options =>
{
    options.SignIn.RequireConfirmedPhoneNumber = false;
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedAccount = false;

    options.Password.RequireUppercase = false;
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 1;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<DreamsContext>();
    dataContext.Database.EnsureCreated();
}


if (!app.Environment.IsDevelopment())
{
    app.UseForwardedHeaders(new ForwardedHeadersOptions
    {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
    });
    app.UseExceptionHandler("/Home/Error");
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();