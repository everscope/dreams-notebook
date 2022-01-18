using DreamWeb.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc();
builder.Services.AddControllersWithViews(); 
builder.Services.AddScoped<DreamInput, DreamInput>();
builder.Services.AddDbContext<DreamsContext>(options =>
    options.UseSqlServer("server = SCAT\\SQLEXPRESS; database = dreams_web; Trusted_Connection=True ; MultipleActiveResultSets = true"));
builder.Services.AddDefaultIdentity<UserAccount>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<DreamsContext>();
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