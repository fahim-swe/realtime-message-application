using api.Helper;
using Microsoft.EntityFrameworkCore;
using repository.Imp.StoreContext;
using repository.Imp.UnitOfWork;
using repository.Interfaces;
using AutoMapper;

namespace api.Extensions
{
    public static class ApplicationServices
    {
        public static IServiceCollection AddDataBaseServices(this IServiceCollection services, IConfiguration _config)
        {
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            
            services.AddDbContext<DataContext>(options =>
            {
                string mySqlConnectionStr = _config.GetConnectionString("DefaultConnection");
                options.UseMySql(mySqlConnectionStr, ServerVersion.AutoDetect(mySqlConnectionStr),
                 optionsBuilder => optionsBuilder.MigrationsAssembly("api"));
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}