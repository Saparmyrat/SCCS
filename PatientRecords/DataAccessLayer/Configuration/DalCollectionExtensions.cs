using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccessLayer.Configuration
{
    public static class DalCollectionExtensions
    {
        public static void RegisterDependencies(IConfiguration configuration, IServiceCollection services, string connectionName)
        {
            string connection = configuration.GetConnectionString(connectionName);
            services.AddDbContext<PatientRecordsContext>(options => options.UseSqlServer(connection));

            services.AddScoped<IRepository<DiseasedDto, int>, GenericRepository<DiseasedDto>>();
            services.AddScoped<IRepository<DoctorDto, int>, GenericRepository<DoctorDto>>();
            services.AddScoped<IRepository<PatientDto, int>, GenericRepository<PatientDto>>();
            services.AddScoped<IRepository<RecordDto, int>, GenericRepository<RecordDto>>();
        }
    }
}
