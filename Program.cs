using System.Runtime.CompilerServices;
using System.Text;
using BLL.Interfaces;
using BLL.Services;
using DAL.Context;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);


// Remarque : la clef secrete est dans l'appsettings.json
// on utilise toujours la même clef secrete (dans differentes APIs)
// permet de decoder notre token et verifier sa validité 

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidIssuer = builder.Configuration["jwt:issuer"],
                
                ValidateAudience = true,
                ValidAudience = builder.Configuration["jwt:issuer"],
                
                ValidateLifetime = true,
                
                
                // clef signée que le token va contenir => determine si le token est valide ou pas
                // On doit convertir en UTF8 notre tableau de bytes (la clef secrète)
                
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["jwt:key"]))
                

            };
        });

// Add services to the container.

builder.Services.AddDbContext<AuthDbContext>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IJwtService, JwtService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(options =>
    {
        options.AllowAnyOrigin();
        options.AllowAnyHeader();
        options.WithMethods("GET", "POST", "PUT", "PATCH", "DELETE");
        
    });


});

var app = builder.Build();

app.UseCors("GetOnly");

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