using ContosoUniversity.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<SchoolContext>(options
=> options.UseSqlServer(builder.Configuration.GetConnectionString("Con")));

builder.Services.AddSession(ss =>
{
    ss.IdleTimeout = TimeSpan.FromSeconds(30);
    ss.Cookie.IsEssential = true;
    ss.Cookie.HttpOnly = true;
});


// Add authentication services and specify the default scheme

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.LoginPath = "/Employee/Login"; // Path to the login page
            options.AccessDeniedPath = "/Account/AccessDenied"; // Path to the access denied page
        });

// Add authorization services
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireLoggedIn", policy => policy.RequireAuthenticatedUser());
});


var app = builder.Build();
var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetService<SchoolContext>();
DbInitializer.Initialize(context);




// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
///midel wires
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
