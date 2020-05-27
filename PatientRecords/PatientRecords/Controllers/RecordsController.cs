using BusinessLayer.Entities;
using BusinessLayer.Exceptions;
using BusinessLayer.Interfaces;
using EasyConsole;
using PatientRecords.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

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
              .Add("Display all records", () => DisplayAll().GetAwaiter().GetResult())
              .Add("Display record by id", () => DisplayById().GetAwaiter().GetResult())
              .Add("Create record", () => Create().GetAwaiter().GetResult())
              .Add("Update record", () => Update().GetAwaiter().GetResult())
              .Add("Delete record", () => Delete().GetAwaiter().GetResult());

            menu.Display();
        }

        public async Task DisplayAll()
        {
            var allRecords = await _recordService.GetAllAsync();

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

        public async Task DisplayById()
        {
            Console.WriteLine("Record by id");
            try
            {
                Console.Write("Indicate id: ");
                var id = int.Parse(Console.ReadLine());
                var record = await _recordService.GetByIdAsync(id);

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

        public async Task Create()
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

                await _recordService.CreateAsync(record);
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

        public async Task Update()
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

                await _recordService.UpdateAsync(record);
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

        public async Task Delete()
        {
            Console.WriteLine("Delete record");
            try
            {
                Console.WriteLine("Indicate id: ");
                int id = int.Parse(Console.ReadLine());

                var allRecords = await _recordService.GetAllAsync();
                var record = allRecords.Where(val => val.Id == id).FirstOrDefault();

                await _recordService.DeleteAsync(record);
                Console.WriteLine("Record deleted succesfully");
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
