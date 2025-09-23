using Aplication.Interfaces;
using Aplication;
using Infraestructure.Commands;
using Infraestructure.Persistence;
using Infraestructure.Querys;
using Microsoft.EntityFrameworkCore;
using Aplication.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
builder.Services.AddAuthorization();

// custon (Inyecto)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString)); //Uso PostgreSQL

builder.Services.AddScoped<ICategoryServices, CategoryServices>();
builder.Services.AddScoped<ICategoryQuery, CategoryQuery>();
builder.Services.AddScoped<ICategoryCommand, CategoryCommand>();

builder.Services.AddScoped<IDeliveryServices, DeliveryServices>();
builder.Services.AddScoped<IDeliveryQuery, DeliveryQuery>();
builder.Services.AddScoped<IDeliveryCommand, DeliveryCommand>();

builder.Services.AddScoped<IStatusServices, StatusServices>();
builder.Services.AddScoped<IStatusQuery, StatusQuery>();
builder.Services.AddScoped<IStatusCommand, StatusCommand>();

builder.Services.AddScoped<IDishServices, DishServices>();
builder.Services.AddScoped<IDishQuery, DishQuery>();
builder.Services.AddScoped<IDishCommand, DishCommand>();


builder.Services.AddTransient<IServicesGetAll, ServicesGetAll>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<RestauranteApi.Middleware.ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


