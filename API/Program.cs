using Microsoft.EntityFrameworkCore;
using Data.Context;
using Services.CRMServices;
using Core;
using Data.GenericRepo;
using Services.UtilitySerivices;
using API.Controllers.GenericController;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add Jwt
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// Add DB Context
builder.Services.AddDbContext<CRMDBContext>(options =>
options.UseSqlServer(connectionString));

//Defining all entityes that should use generic controller
var entitys = new[]
{
    typeof(Lead),
    typeof(Core.Task),
    typeof(User)
};

foreach(var type in entitys)
{
    //Adding Transient for Generic Controller.
    var controllerType = typeof(GenericController<>).MakeGenericType(type);
    builder.Services.AddTransient(controllerType);

    //Addeding Scope for Generic Repository interface and implementation.
    var repoType = typeof(IGenericRepository<>).MakeGenericType(type);
    var repoImp = typeof(GenericRepository<>).MakeGenericType(type);
    builder.Services.AddScoped(repoType, repoImp);

    //Adding Scope for Generic Crud Services interface and implementation.
    var serviceType = typeof(IGenericCrud<>).MakeGenericType(type);
    var serviceImp = typeof(GenericCrud<>).MakeGenericType(type);
    builder.Services.AddScoped(serviceType, serviceImp);
}

builder.Services.AddAuthorization();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/*
 * Add Scope For the Projects
 */
//Adding Scope for Generic Crud Services interface and implementation.
//builder.Services.AddScoped<CrudServices>();
//builder.Services.AddScoped<Lead>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
