using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using WeatherStationMVC.Data;
using WeatherStation.Entity;


var builder = WebApplication.CreateBuilder(args);
//
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<AuthDbContext>(options =>
{
    //options.UseSqlite(builder.Configuration.GetConnectionString("AuthDbContextConnection"),
    //                b => b.MigrationsAssembly("WeatherStationMVC"));
    options.UseSqlite(builder.Configuration.GetConnectionString("AuthDbContextConnection"));
});

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<AuthDbContext>();

builder.Services.AddRazorPages();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    
}
else 
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
