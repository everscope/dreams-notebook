using DreamWeb;
using DreamWeb.DAL;
using DreamWeb.DAL.Entities;
using DreamWeb.DreamPublicationSorting;
using DreamWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc();
builder.Services.AddControllersWithViews(); 
builder.Services.AddScoped<DreamInputModel, DreamInputModel>();
builder.Services.AddDbContext<DreamsContext>(options =>
    options.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;Integrated Security=True"));
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

using (var context = new DreamsContext (new DbContextOptionsBuilder<DreamsContext>()
    .UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;Integrated Security=True").Options))
{
    context.Database.EnsureCreated();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
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