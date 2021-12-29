using DreamWeb.Models;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

//services
builder.Services.AddControllersWithViews(); 
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddDbContext<DreamsContext>(options =>
    options.UseSqlServer("server = SCAT\\SQLEXPRESS; database = dreams_web; Trusted_Connection=True ; MultipleActiveResultSets = true"));

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