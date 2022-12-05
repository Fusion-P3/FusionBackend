using ECommerce.Data;
using Microsoft.AspNetCore.DataProtection.Repositories;
using ECommerce.Data.Entities;
using Microsoft.EntityFrameworkCore;
using ECommerce.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          // Not safe but clears CORS issue.. we can setup a specific orgin when deploying
                          policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
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

app.UseSwagger();
app.UseSwaggerUI();



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(MyAllowSpecificOrigins);
app.Run();
