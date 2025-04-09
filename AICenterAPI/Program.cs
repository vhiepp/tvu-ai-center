
using AICenterAPI.Configurations;
using Microsoft.EntityFrameworkCore;
using UltraBusAPI.Configurations;
using UltraBusAPI.Datas;

namespace AICenterAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            DatabaseConfig.AddDbContext(builder);

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

            app.UseCors("AllowAll");

            app.UseAuthorization();

            // Gọi Seeder
            SeederConfig.Run(app.Services);

            app.MapControllers();

            app.Run();
        }
    }
}
