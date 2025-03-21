using CoreBusiness.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SQLServerPlugin;
using SQLServerPlugin.Repositories;
using System.Text;
using UserCase.Business;
using UserCase.IBusiness;
using UserCase.IRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddDbContext<CustomDBContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("SqlServerConnectionString")
    ));
var configuration = Configuration.GetInstance();
configuration.JWTKey = builder.Configuration.GetValue<string>("JWTSetting:Key");

builder.Services.AddAuthentication(optiones =>
{
    optiones.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    optiones.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    optiones.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    //ValidIssuer = "https://localhost:7264",
                    //ValidAudience = "https://localhost:7264",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTSetting:Key"])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = false,
                    ClockSkew = TimeSpan.Zero,
                };
            });

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPIv7", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description =
            "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
});

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
