using CustomerApplication.Core.Interfaces;
using CustomerApplication.Core.Services;
using CustomerApplication.Domain.DTO;
using CustomerApplication.Infrastructure.DbContexts;
using CustomerApplication.Infrastructure.Interfaces;
using CustomerApplication.Infrastructure.Repos;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace CustomerApplication.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            ConfigurationManager configuration = builder.Configuration;            

            builder.Services.AddDbContext<CustomerApplicationContext>(options =>
            options.UseCosmos(
                builder.Configuration.GetSection("CosmosDB:URI").Value.ToString(),
                builder.Configuration.GetSection("CosmosDB:Primary Key").Value.ToString(),
                builder.Configuration.GetSection("CosmosDB:Database").Value.ToString()));


            builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


            builder.Services.AddControllers();
            builder.Services.AddTransient<ICustomerService, CustomerService>();
            builder.Services.AddTransient<ICustomerRepo, CustomerRepo>();
            builder.Services.AddTransient<ISalesService, SalesService>();
            builder.Services.AddTransient<ISalesRepo, SalesRepo>();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(typeof(SalesProfile).Assembly);
            builder.Services.AddAutoMapper(typeof(CustomerProfile).Assembly);


            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.MapControllers();            

            app.Run();
        }
    }
}