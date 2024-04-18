using AutoMapper;
using EcommerceApi;
using EcommerceApi.Mappings;
using EcommerceApi.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("EcommerceApiDbConnection")));

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


builder.Services.AddControllers();

var mapperConfiguration = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(typeof(AppMapper));
});

var mapper = mapperConfiguration.CreateMapper();

builder.Services.AddSingleton(mapper);

//DbContext as a service
builder.Services.AddScoped<AppDbContext>();

//Product Service 
builder.Services.AddScoped<ProductService>();

//User Service 
builder.Services.AddScoped<UserService>();

//JWt Service
builder.Services.AddScoped<JwtService>();

//Auth Service
builder.Services.AddScoped<AuthService>();

//Category Service
builder.Services.AddScoped<CategoryService>();

//Address Service
builder.Services.AddScoped<AddressService>();

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options => {
    options.AddPolicy(name: MyAllowSpecificOrigins,
      policy => {
          policy.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod();
      });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseStaticFiles();

app.MapControllers();

app.Run();
