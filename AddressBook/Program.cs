using AutoMapper;
using BusinessLayer.Interface;
using BusinessLayer.Service;
using FluentValidation;
using Mapping.Mapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Context;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using System.Text;
using Validation.Validator;

var builder = WebApplication.CreateBuilder(args);

var jwtKey = builder.Configuration["Jwt:Key"];
var jwtIssuer = builder.Configuration["Jwt:Issuer"];
var jwtAudience = builder.Configuration["Jwt:Audience"];

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<IAddressBookBL, AddressBookBL>();
builder.Services.AddScoped<IUserRL, UserRL>();
builder.Services.AddScoped<IUserBL, UserBL>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<IEmailService, EmailService>();

//Database Connection
builder.Services.AddDbContext<AddressBookContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));

//AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Register FluentValidation for AddressBook DTO validation
builder.Services.AddValidatorsFromAssemblyContaining<AddressBookValidator>();

// Configure JWT authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
