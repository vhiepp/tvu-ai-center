
using AICenterAPI.Configurations;
using Microsoft.EntityFrameworkCore;
using AICenterAPI.Datas;
using UltraBusAPI.Configurations;
using AICenterAPI.Extensions;

namespace AICenterAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            DatabaseConfig.AddDbContext(builder);

            RepositoryConfig.AddRepositorys(builder.Services);

            ServiceConfig.AddServices(builder.Services);

            JwtConfig.AddJwtConfig(builder.Services, builder.Configuration);

            CorsConfig.AddCorsConfig(builder.Services);

            SwaggerConfig.AddSwaggerGen(builder.Services);

            builder.Services.AddAppLocalization();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddHttpClient();

            var app = builder.Build();

            FileConfig.AddPublicFolder(app);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("AllowAll");

            app.UseAuthorization();

            MiddlewareConfig.AddMiddleware(app);

            // Gọi Seeder
            SeederConfig.Run(app.Services);

            app.MapControllers();

            app.Run();
        }
    }
}
