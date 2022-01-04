using DreamWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

//services
//builder.Services.AddAuthentication(sharedOptions =>
//{
//    sharedOptions.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//    sharedOptions.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//    // sharedOptions.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
//}).AddCookie(options =>
//{
//    options.LoginPath = "/SignIn";
//}

//    );

//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie(options =>
//    {
//        options.LoginPath = "/SignIn";
//    }

//    );

//builder.Services.AddIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false).Add
builder.Services.AddMvc();
builder.Services.AddControllersWithViews(); 
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddDbContext<DreamsContext>(options =>
    options.UseSqlServer("server = SCAT\\SQLEXPRESS; database = dreams_web; Trusted_Connection=True ; MultipleActiveResultSets = true"));
builder.Services.AddDefaultIdentity<UserAccount>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<DreamsContext>();
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();