using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ejer2API_AlejandroSerafinParedes.Data;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<Ejer2API_AlejandroSerafinParedesContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Ejer2API_AlejandroSerafinParedesContext") ?? throw new InvalidOperationException("Connection string 'Ejer2API_AlejandroSerafinParedesContext' not found.")));

//----------------------------------------------------------------------------------------------------------------------------------
builder.Services.AddDbContext<UsersContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("UsersDb") ??
throw new InvalidOperationException("Connection string'UsersContext' not found.")));

builder.Services.AddIdentityCore<IdentityUser>(options =>
 options.SignIn.RequireConfirmedEmail = false)
 .AddRoles<IdentityRole>()
 .AddEntityFrameworkStores<UsersContext>();
//----------------------------------------------------------------------------------------------------------------------------------

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
 .AddJwtBearer(options =>
 {
     options.RequireHttpsMetadata = false;
     options.SaveToken = true;
     options.TokenValidationParameters = new TokenValidationParameters
     {
         ValidateIssuer = true,
         ValidateAudience = true,
         ValidateLifetime = true,
         ValidateIssuerSigningKey = true,
         ValidIssuer = builder.Configuration["Jwt:Issuer"],
         ValidAudience = builder.Configuration["Jwt:Audience"],
         IssuerSigningKey = new
    SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
     };
 });

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(
    x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
