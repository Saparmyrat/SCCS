using BusinessLayer.Entities;
using BusinessLayer.Interfaces;
using EasyConsole;
using PatientRecords.Interfaces;
using System;
using System.Linq;


namespace PatientRecords.Controllers
{
    public class DoctorsController : IController
    {
        private readonly IService<Doctor, int> _doctorService;

        public DoctorsController(IService<Doctor, int> doctorService)
        {
            _doctorService = doctorService;
        }

        public void Start()
        {
            Console.WriteLine("Doctor");

            var menu = new Menu()
              .Add("Display all doctors", () => DisplayAll())
              .Add("Display doctor by id", () => DisplayById())
              .Add("Create doctor", () => Create())
              .Add("Update doctor", () => Update())
              .Add("Delete doctor", () => Delete());

            menu.Display();
        }

        public void DisplayAll()
        {
            var allDoctors = _doctorService.GetAllAsync().GetAwaiter().GetResult();

            Console.WriteLine("All doctors");
            foreach (var doctor in allDoctors)
            {
                Console.Write("First name: ");
                Output.WriteLine(ConsoleColor.Green, doctor.FirstName);
                Console.Write("Surname: ");
                Output.WriteLine(ConsoleColor.Green, doctor.Surname);
                Console.Write("Patronic: ");
                Output.WriteLine(ConsoleColor.Green, doctor.Patronic);
                Console.Write("Specialty: ");
                Output.WriteLine(ConsoleColor.Green, doctor.Specialty);
            }
        }

        public void DisplayById()
        {
            Console.WriteLine("Doctor by id");
            try
            {
                Console.Write("Indicate id: ");
                var id = int.Parse(Console.ReadLine());
                var doctor = _doctorService.GetByIdAsync(id).GetAwaiter().GetResult();

                Console.Write("First name: ");
                Output.WriteLine(ConsoleColor.Green, doctor.FirstName);
                Console.Write("Surname: ");
                Output.WriteLine(ConsoleColor.Green, doctor.Surname);
                Console.Write("Patronic: ");
                Output.WriteLine(ConsoleColor.Green, doctor.Patronic);
                Console.Write("Specialty: ");
                Output.WriteLine(ConsoleColor.Green, doctor.Specialty);
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Create()
        {
            var doctor = new Doctor();

            Console.WriteLine("Create doctor");
            try
            {
                Console.WriteLine("First name: ");
                doctor.FirstName = Console.ReadLine();
                Console.WriteLine("Surname: ");
                doctor.Surname = Console.ReadLine();
                Console.WriteLine("Patronic: ");
                doctor.Patronic = Console.ReadLine();
                Console.WriteLine("Specialty: ");
                doctor.Specialty = Console.ReadLine();

                _doctorService.CreateAsync(doctor).GetAwaiter().GetResult();
                Console.WriteLine("Doctor created succesfully");
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Update()
        {
            var doctor = new Doctor();

            Console.WriteLine("Update doctor");
            try
            {
                Console.WriteLine("Indicate id: ");
                doctor.Id = int.Parse(Console.ReadLine());
                Console.WriteLine("First name: ");
                doctor.FirstName = Console.ReadLine();
                Console.WriteLine("Surname: ");
                doctor.Surname = Console.ReadLine();
                Console.WriteLine("Patronic: ");
                doctor.Patronic = Console.ReadLine();
                Console.WriteLine("Specialty: ");
                doctor.Specialty = Console.ReadLine();

                _doctorService.UpdateAsync(doctor).GetAwaiter().GetResult();
                Console.WriteLine("Doctor updates succesfuly");
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Delete()
        {
            Console.WriteLine("Doctor diseased");
            try
            {
                Console.WriteLine("Indicate id: ");
                int id = int.Parse(Console.ReadLine());

                var allDoctors = _doctorService.GetAllAsync().GetAwaiter().GetResult();
                var doctor = allDoctors.Where(val => val.Id == id).FirstOrDefault();

                _doctorService.DeleteAsync(doctor).GetAwaiter().GetResult();
                Console.WriteLine("Doctor deleted succesfully");
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
