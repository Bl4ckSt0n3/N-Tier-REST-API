using forceget.DataAccess.Models.Dtos;
using forceget.Services.Abstract;
using forceget.Services.Services;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using forcegetAPI;
using forceget.DataAccess.Models.DbContexts;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(o => o.AddPolicy("ANGULAR", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));

builder.Services.AddControllers();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IOfferService, OfferService>();

builder.Services.AddDbContext<UsersDbContext>(options => {
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreConnectionString"));
}, ServiceLifetime.Transient);

builder.Services.AddDbContext<DimensionsDbContext>(options => {
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreConnectionString"));
}, ServiceLifetime.Transient);

builder.Services.AddDbContext<OffersDbContext>(options => {
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreConnectionString"));
}, ServiceLifetime.Transient);


builder.Services.AddSwaggerGen( option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Product API Demo", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    options => {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
            .GetBytes(builder.Configuration.GetSection("Token:Key").Value)), 
            ValidateIssuer = false,
            ValidateAudience = false
        };
    }
);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthorization();

app.MapControllers();

app.UseCors("ANGULAR");

app.UseHttpsRedirection();

app.Run();
