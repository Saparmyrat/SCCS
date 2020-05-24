using BusinessLayer.Entities;
using BusinessLayer.Exceptions;
using BusinessLayer.Interfaces;
using EasyConsole;
using PatientRecords.Interfaces;
using System;
using System.Linq;

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
              .Add("Display all diseaseds", () => DisplayAll())
              .Add("Display diseased by id", () => DisplayById())
              .Add("Create diseased", () => Create())
              .Add("Update diseased", () => Update())
              .Add("Delete diseased", () => Delete());

            menu.Display();
        }

        public void DisplayAll()
        {
            var allDiseaseds = _diseasedService.GetAllAsync().GetAwaiter().GetResult();

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

        public void DisplayById()
        {
            Console.WriteLine("Diseased by id");
            try
            {
                Console.Write("Indicate id: ");
                var id = int.Parse(Console.ReadLine());
                var diseased = _diseasedService.GetByIdAsync(id).GetAwaiter().GetResult();

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

        public void Create()
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

                _diseasedService.CreateAsync(diseased).GetAwaiter().GetResult();
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

        public void Update()
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

                _diseasedService.UpdateAsync(diseased).GetAwaiter().GetResult();
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

        public void Delete()
        {
            Console.WriteLine("Delete diseased");
            try
            {
                Console.WriteLine("Indicate id: ");
                int id = int.Parse(Console.ReadLine());

                var allDiseased = _diseasedService.GetAllAsync().GetAwaiter().GetResult();
                var diseased = allDiseased.Where(val => val.Id == id).FirstOrDefault();

                _diseasedService.DeleteAsync(diseased).GetAwaiter().GetResult();
                Console.WriteLine("Diseased deleted succesfully");
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
