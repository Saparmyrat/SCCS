using BusinessLayer.Entities;
using BusinessLayer.Interfaces;
using EasyConsole;
using PatientRecords.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

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
              .Add("Display all doctors", () => DisplayAll().GetAwaiter().GetResult())
              .Add("Display doctor by id",  () => DisplayById().GetAwaiter().GetResult())
              .Add("Create doctor", () => Create().GetAwaiter().GetResult())
              .Add("Update doctor", () => Update().GetAwaiter().GetResult())
              .Add("Delete doctor", () => Delete().GetAwaiter().GetResult());

            menu.Display();
        }

        public async Task DisplayAll()
        {
            var allDoctors = await _doctorService.GetAllAsync();

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

        public async Task DisplayById()
        {
            Console.WriteLine("Doctor by id");
            try
            {
                Console.Write("Indicate id: ");
                var id = int.Parse(Console.ReadLine());
                var doctor = await _doctorService.GetByIdAsync(id);

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

        public async Task Create()
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

                await _doctorService.CreateAsync(doctor);
                Console.WriteLine("Doctor created succesfully");
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task Update()
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

                await _doctorService.UpdateAsync(doctor);
                Console.WriteLine("Doctor updates succesfuly");
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task Delete()
        {
            Console.WriteLine("Doctor diseased");
            try
            {
                Console.WriteLine("Indicate id: ");
                int id = int.Parse(Console.ReadLine());

                var allDoctors = await _doctorService.GetAllAsync();
                var doctor = allDoctors.Where(val => val.Id == id).FirstOrDefault();

                await _doctorService.DeleteAsync(doctor);
                Console.WriteLine("Doctor deleted succesfully");
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
