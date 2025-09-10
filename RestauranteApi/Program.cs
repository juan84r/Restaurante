using Aplication.Interfaces;
using Aplication.UseCase.Restaurante;
using Aplication.UseCase.Restaurante.GetAll;
using Infraestructure.Command;
using Infraestructure.Persistence;
using Infraestructure.Querys;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
builder.Services.AddAuthorization();

// custon (Inyecto)
var connectionString = builder.Configuration["ConnectionString"];
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

builder.Services.AddTransient<IServicesGetAll, ServicesGetAll>();


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


