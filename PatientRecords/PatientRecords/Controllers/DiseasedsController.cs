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
    public class DiseasedsController : IController
    {
        private readonly IService<Diseased, int> _diseasedService;

        public DiseasedsController(IService<Diseased, int> diseasedService)
        {
            _diseasedService = diseasedService;
        }

        public void Start()
        {
            Console.WriteLine("Diseased");

            var menu = new Menu()
              .Add("Display all diseaseds", () => DisplayAll().GetAwaiter().GetResult())
              .Add("Display diseased by id", () => DisplayById().GetAwaiter().GetResult())
              .Add("Create diseased", () => Create().GetAwaiter().GetResult())
              .Add("Update diseased", () => Update().GetAwaiter().GetResult())
              .Add("Delete diseased", () => Delete().GetAwaiter().GetResult());

            menu.Display();
        }

        public async Task DisplayAll()
        {
            var allDiseaseds = await _diseasedService.GetAllAsync();

            Console.WriteLine("All diseaseds");
            foreach (var diseased in allDiseaseds)
            {
                Console.Write("Disease: ");
                Output.WriteLine(ConsoleColor.Green, diseased.Disease);
                Console.Write("Patient Id: ");
                Output.WriteLine(ConsoleColor.Green, diseased.PatientId.ToString());
                Console.Write("Date of illnes (yy-mm-dd):");
                Output.WriteLine(ConsoleColor.Green, diseased.DateOfIllnes.ToString());
            }
        }

        public async Task DisplayById()
        {
            Console.WriteLine("Diseased by id");
            try
            {
                Console.Write("Indicate id: ");
                var id = int.Parse(Console.ReadLine());
                var diseased = await _diseasedService.GetByIdAsync(id);

                Console.Write("Disease: ");
                Output.WriteLine(ConsoleColor.Green, diseased.Disease);
                Console.Write("Date of illnes (yy-mm-dd):");
                Output.WriteLine(ConsoleColor.Green, diseased.DateOfIllnes.ToString());
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task Create()
        {
            var diseased = new Diseased();

            Console.WriteLine("Create diseased");
            try
            {
                Console.WriteLine("Disease: ");
                diseased.Disease = Console.ReadLine();
                Console.WriteLine("Patient Id: ");
                diseased.PatientId = int.Parse(Console.ReadLine());
                Console.WriteLine("Date of illnes (yy-mm-dd):");
                diseased.DateOfIllnes = DateTimeOffset.Parse(Console.ReadLine());

                await _diseasedService.CreateAsync(diseased);
                Console.WriteLine("Diseased created succesfully");
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
            var diseased = new Diseased();

            Console.WriteLine("Update diseased");
            try
            {
                Console.WriteLine("Indicate id: ");
                diseased.Id = int.Parse(Console.ReadLine());
                Console.WriteLine("Disease: ");
                diseased.Disease = Console.ReadLine();
                Console.WriteLine("Patient Id: ");
                diseased.PatientId = int.Parse(Console.ReadLine());
                Console.WriteLine("Date of illnes (yy-mm-dd):");
                diseased.DateOfIllnes = DateTimeOffset.Parse(Console.ReadLine());

                await _diseasedService.UpdateAsync(diseased);
                Console.WriteLine("Diseased updates succesfuly");
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
            Console.WriteLine("Delete diseased");
            try
            {
                Console.WriteLine("Indicate id: ");
                int id = int.Parse(Console.ReadLine());

                var allDiseased = await _diseasedService.GetAllAsync();
                var diseased = allDiseased.Where(val => val.Id == id).FirstOrDefault();

                await _diseasedService.DeleteAsync(diseased);
                Console.WriteLine("Diseased deleted succesfully");
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
