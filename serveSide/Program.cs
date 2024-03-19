using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PomeloDB>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("PomeloDB") ?? throw new InvalidOperationException("Connection string 'PomeloDB' not found."), MariaDbServerVersion.AutoDetect(builder.Configuration.GetConnectionString("PomeloDB"))));



// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Ranks}/{action=Index}/{id?}");

app.Run();
