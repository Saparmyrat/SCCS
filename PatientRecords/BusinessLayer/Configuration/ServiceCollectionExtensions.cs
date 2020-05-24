using BusinessLayer.Entities;
using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using DataAccessLayer.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace BusinessLayer.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterDependencies(IConfiguration configuration, IServiceCollection services, string connectionName)
        {
            DalCollectionExtensions.RegisterDependencies(configuration, services, connectionName);

            services.AddScoped<IService<Diseased, int>, DiseasedService>();
            services.AddScoped<IService<Doctor, int>, DoctorService>();
            services.AddScoped<IService<Patient, int>, PatientService>();
            services.AddScoped<IService<Record, int>, RecordService>();
        }
    }
}
