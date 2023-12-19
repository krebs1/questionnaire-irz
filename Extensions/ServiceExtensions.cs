using Contracts;
using Microsoft.EntityFrameworkCore;
using Entities;
using Repository;

namespace testx.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }

        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options =>
            {

            });
        }
        
        public static void ConfigurePgContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config["pgconnection:connectionString"];

            services.AddDbContext<RepositoryContext>(o => o.UseNpgsql(connectionString));
        }

        public static void ConfigureRepositoryWrapper(this IServiceCollection services) 
        { 
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>(); 
        }
    }
}
