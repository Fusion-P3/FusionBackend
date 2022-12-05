using ECommerce.Data;
using Microsoft.AspNetCore.DataProtection.Repositories;
using ECommerce.Data.Entities;
using Microsoft.EntityFrameworkCore;
using ECommerce.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowCredentials();
        });
});

var connectionString = builder.Configuration["ECommerce:ConnectionString"];

// builder.Services.AddSingleton<IRepository>
//     (sp => new EFRepositoryAccess(connectionString, sp.GetRequiredService<ILogger<EFRepositoryAccess>>()));
builder.Services.AddDbContext<fusionContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("FusionDB")));
builder.Services.AddScoped<IRepository, EFRepositoryAccess>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICheckoutService, CheckoutService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IInventoryService, InventoryService>();

builder.Services.AddControllers();

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

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
