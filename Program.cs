using Intranet_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Cors.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add database to the container.
builder.Services.AddDbContext<IntranetDbContext>(options => options.UseSqlite(configuration.GetConnectionString("IntranetDb")));

// Add JWT & Authentication

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt => {
    opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters() {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "https://localhost:7198",
        ValidAudience = "http://localhost:8080",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("15b9722f46ab79c34be75be40438bf4f0b80962bb1c9a0921ace4fc561b24740"))
    };
});

var allowedCorsOrigins = "_allowedOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: allowedCorsOrigins,
        policy  =>
        {
            policy.AllowAnyHeader();
            policy.AllowAnyOrigin();
        });
});

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseCors(allowedCorsOrigins);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
