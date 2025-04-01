using Esms.Business.Repositories;
using Esms.Business.Services;
using Esms.Business.Services.V1;
using Esms.SQLite;
using Esms.SQLite.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Esms.DependencyInjection
{
    public static class Config
    {

        public static IServiceProvider ApplyDependencies(this IServiceProvider app)
        {

            using (var scope = app.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<EsmsDbContext>();
                db.Database.Migrate();
            }
            return app;
        }

        public static IServiceCollection ResolveDependencies(this IServiceCollection services, IConfiguration Configuration)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<EsmsDbContext>(options => {
                options.UseSqlite(connectionString);
            });



            services.AddScoped(typeof(IServiceBase<>), typeof(ServiceBase<>));
            services.AddScoped<IEmployeesAddressesService, EmployeesAddressesService>();
            services.AddScoped<IEmployeeSeriesService, EmployeeSeriesService>();

            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            return services;
        }
    }
}
