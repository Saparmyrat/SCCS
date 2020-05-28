using BusinessLayer.Entities;
using BusinessLayer.Interfaces;
using EasyConsole;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PatientRecords.Controllers;
using System;
using System.IO;

namespace PatientRecords
{
    public static class Program
    {
        private static IServiceProvider _serviceProvider;

        public static void Main(string[] args)
        {
            RegisterServices();

            StartSession();

            DisposeServices();
            Console.ReadKey();
        }

        private static void RegisterServices()
        {
            var serviceCollection = new ServiceCollection();
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory() + @"\..\..\..\")
                .AddJsonFile("appsettings.json", false);
            var config = configBuilder.Build();

            string connectionName = "dbConnection";

            Startup.ConfigureServices(config, serviceCollection, connectionName);
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        private static void DisposeServices()
        {
            if (_serviceProvider != null)
            {
                if (_serviceProvider is IDisposable)
                {
                    ((IDisposable)_serviceProvider).Dispose();
                }
            }
        }

        private static void WorkWithDiseased()
        {
            var service = _serviceProvider.GetRequiredService<IService<Diseased, int>>();
            var controller = new DiseasedsController(service);
            controller.Start();
        }

        private static void WorkWithDoctor()
        {
            var service = _serviceProvider.GetRequiredService<IService<Doctor, int>>();
            var controller = new DoctorsController(service);
            controller.Start();
        }

        private static void WorkWithPatient()
        {
            var service = _serviceProvider.GetRequiredService<IService<Patient, int>>();
            var controller = new PatientsController(service);
            controller.Start();
        }

        private static void WorkWithRecord()
        {
            var service = _serviceProvider.GetRequiredService<IService<Record, int>>();
            var controller = new RecordsController(service);
            controller.Start();
        }

        public static void StartSession()
        {
            var menu = new Menu()
              .Add("Work with Diseased", () => WorkWithDiseased())
              .Add("Work with Doctor", () => WorkWithDoctor())
              .Add("Work with Patient", () => WorkWithPatient())
              .Add("Work with Record", () => WorkWithRecord());

            menu.Display();
        }
    }
}
