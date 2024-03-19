using API.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
//using API.Data;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PomeloDB>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("PomeloDB") ?? throw new InvalidOperationException("Connection string 'PomeloDB' not found."), MariaDbServerVersion.AutoDetect(builder.Configuration.GetConnectionString("PomeloDB"))));
builder.Services.AddCors(options =>
{
    options.AddPolicy("Allow All",
        builder =>
        {
            builder.SetIsOriginAllowed(param => true).AllowAnyMethod().AllowAnyHeader().AllowCredentials();
        });
}
);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(ops => {
    ops.RequireHttpsMetadata = false;
    ops.SaveToken = true;
    ops.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audience"],
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))};
});

builder.Services.AddSignalR();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("Allow All");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<MyHub>("hubs/chat");


app.Run();
