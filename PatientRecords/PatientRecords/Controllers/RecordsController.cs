using BusinessLayer.Entities;
using BusinessLayer.Exceptions;
using BusinessLayer.Interfaces;
using EasyConsole;
using PatientRecords.Interfaces;
using System;
using System.Linq;

namespace PatientRecords.Controllers
{
    public class RecordsController : IController
    {
        private readonly IService<Record, int> _recordService;

        public RecordsController(IService<Record, int> recordService)
        {
            _recordService = recordService;
        }

        public void Start()
        {
            Console.WriteLine("Record");

            var menu = new Menu()
              .Add("Display all records", () => DisplayAll())
              .Add("Display record by id", () => DisplayById())
              .Add("Create record", () => Create())
              .Add("Update record", () => Update())
              .Add("Delete record", () => Delete());

            menu.Display();
        }

        public void DisplayAll()
        {
            var allRecords = _recordService.GetAllAsync().GetAwaiter().GetResult();

            Console.WriteLine("All records");
            foreach (var record in allRecords)
            {
                Console.Write("Disease: ");
                Output.WriteLine(ConsoleColor.Green, record.Disease);
                Console.Write("PatientId: ");
                Output.WriteLine(ConsoleColor.Green, record.PatientId.ToString());
                Console.Write("Date of disease: ");
                Output.WriteLine(ConsoleColor.Green, record.DateOfDisease.ToString());
            }
        }

        public void DisplayById()
        {
            Console.WriteLine("Record by id");
            try
            {
                Console.Write("Indicate id: ");
                var id = int.Parse(Console.ReadLine());
                var record = _recordService.GetByIdAsync(id).GetAwaiter().GetResult();

                Console.Write("Disease: ");
                Output.WriteLine(ConsoleColor.Green, record.Disease);
                Console.Write("PatientId: ");
                Output.WriteLine(ConsoleColor.Green, record.PatientId.ToString());
                Console.Write("Date of disease: ");
                Output.WriteLine(ConsoleColor.Green, record.DateOfDisease.ToString());
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Create()
        {
            var record = new Record();

            Console.WriteLine("Create record");
            try
            {
                Console.WriteLine("Disease: ");
                record.Disease = Console.ReadLine();
                Console.WriteLine("Patient Id: ");
                record.PatientId = int.Parse(Console.ReadLine());
                Console.WriteLine("Patronic: ");
                record.DateOfDisease = DateTimeOffset.Parse(Console.ReadLine());

                _recordService.CreateAsync(record).GetAwaiter().GetResult();
                Console.WriteLine("Record created succesfully");
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (DateException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Update()
        {
            var record = new Record();

            Console.WriteLine("Update record");
            try
            {
                Console.WriteLine("Disease: ");
                record.Disease = Console.ReadLine();
                Console.WriteLine("Patient Id: ");
                record.PatientId = int.Parse(Console.ReadLine());
                Console.WriteLine("Patronic: ");
                record.DateOfDisease = DateTimeOffset.Parse(Console.ReadLine());

                _recordService.UpdateAsync(record).GetAwaiter().GetResult();
                Console.WriteLine("Record updates succesfuly");
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (DateException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Delete()
        {
            Console.WriteLine("Delete record");
            try
            {
                Console.WriteLine("Indicate id: ");
                int id = int.Parse(Console.ReadLine());

                var allRecords = _recordService.GetAllAsync().GetAwaiter().GetResult();
                var record = allRecords.Where(val => val.Id == id).FirstOrDefault();

                _recordService.DeleteAsync(record).GetAwaiter().GetResult();
                Console.WriteLine("Record deleted succesfully");
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
