using AutoMapper;
using BusinessLayer.Interface;
using BusinessLayer.Service;
using FluentValidation;
using Mapping.Mapper;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Context;
using Validation.Validator;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IAddressBookBL, AddressBookBL>();

//Database Connection
builder.Services.AddDbContext<AddressBookContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));

//AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Register FluentValidation for AddressBook DTO validation
builder.Services.AddValidatorsFromAssemblyContaining<AddressBookValidator>();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
