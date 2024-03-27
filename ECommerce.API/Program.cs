using ECommerce.Application.DependencyInjection;
using ECommerce.Application.Mappings;
using ECommerce.Infrastructure.Context;
using ECommerce.Infrastructure.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging();

// Add services to the container.
builder.Services.AddCors();
builder.Services.AddControllers()
                .AddJsonOptions(x =>
                {
                    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ECommerce API", Version = "v1" });
});

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddDbContext<ECommerceDbContext>(o =>
{
    o.UseSqlServer(builder.Configuration.GetConnectionString("DbConnectionStrings"));
    o.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

#region Moduler dependancies Resolver

builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();
//builder.Services.AddWebApiServices();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ECommerce API V1");
    });
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
