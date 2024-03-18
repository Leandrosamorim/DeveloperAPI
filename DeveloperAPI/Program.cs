using Microsoft.AspNetCore.Authentication.JwtBearer;
using IoC;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Domain.HttpService.Interfaces;
using Domain.HttpService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddInfrastructure(builder.Configuration, builder.Environment);
builder.Services.AddCors(options =>
{
    options.AddPolicy("MatchAPI", b => b.WithOrigins(builder.Configuration.GetValue<string>("MatchAPI")).AllowAnyHeader().AllowAnyMethod());
    options.AddPolicy("AllowSpecificOrigin", b => b.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().Build());
});

builder.Services.AddHttpClient<IMatchHttpService, MatchHttpService>(client => {
    client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("MatchAPI"));
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Developer-API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Jwt Auth",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme{ Reference = new OpenApiReference
            {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
            }
        },
            new string[]{}
            }


    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:key"]))
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseCors("AllowSpecificOrigin");
app.UseHttpsRedirection();


app.Run();
