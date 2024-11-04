using BoreWellManager.Business.Operations.Document;
using BoreWellManager.Business.Operations.Institution;
using BoreWellManager.Business.Operations.Land;
using BoreWellManager.Business.Operations.Payment;
using BoreWellManager.Business.Operations.Setting;
using BoreWellManager.Business.Operations.User;
using BoreWellManager.Business.Operations.Well;
using BoreWellManager.Data.Context;
using BoreWellManager.Data.Repository;
using BoreWellManager.Data.UnitOfWork;
using BoreWellManager.WebApi.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( options=>
{
    var jwtSecurityScheme = new OpenApiSecurityScheme { 
    
        Scheme = "Bearer",
        BearerFormat = "JWT",
        Name = "Jwt Authentication",
        In= ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "Put **_Only_** your Jwt Bearer Token on TextBox below",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type=ReferenceType.SecurityScheme
        }
    };
    options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
    options.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {jwtSecurityScheme,Array.Empty<string>() }
    });
});

var cn = builder.Configuration.GetConnectionString("default");
builder.Services.AddDbContext<BoreWellManagerDbContext>(options=>options.UseSqlServer(cn));

builder.Services.AddScoped(typeof(IRepository<>),typeof(Repository<>));// generik olduklarý için kullanýmý farklý
builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
builder.Services.AddScoped<IUserService,UserManager>();
builder.Services.AddScoped<ILandService,LandManager>();
builder.Services.AddScoped<ISettingService,SettingManager>();
builder.Services.AddScoped<IWellService,WellManager>();
builder.Services.AddScoped<IInstitutionService, InstitutionManager>();
builder.Services.AddScoped<IDocumentService, DocumentManager>();
builder.Services.AddScoped<IPaymetService, PaymentManager>();

//JWT ÝÞLEMLERÝ
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],

        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],

        ValidateLifetime = true,

        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]!))

    };
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMaintenanceMode();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
