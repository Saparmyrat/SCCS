using BusinessLayer.Entities;
using BusinessLayer.Interfaces;
using EasyConsole;
using PatientRecords.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PatientRecords.Controllers
{
    public class PatientsController : IController
    {
        private readonly IService<Patient, int> _patientService;

        public PatientsController(IService<Patient, int> patientService)
        {
            _patientService = patientService;
        }

        public void Start()
        {
            Console.WriteLine("Patient");

            var menu = new Menu()
              .Add("Display all patients", () => DisplayAll().GetAwaiter().GetResult())
              .Add("Display patient by id", () => DisplayById().GetAwaiter().GetResult())
              .Add("Create patient", () => Create().GetAwaiter().GetResult())
              .Add("Update patient", () => Update().GetAwaiter().GetResult())
              .Add("Delete patient", () => Delete().GetAwaiter().GetResult());

            menu.Display();
        }

        public async Task DisplayAll()
        {
            var allPatients = await _patientService.GetAllAsync();

            Console.WriteLine("All patients");
            foreach (var patient in allPatients)
            {
                Console.Write("First name: ");
                Output.WriteLine(ConsoleColor.Green, patient.FirstName);
                Console.Write("Surname: ");
                Output.WriteLine(ConsoleColor.Green, patient.Surname);
                Console.Write("Patronic: ");
                Output.WriteLine(ConsoleColor.Green, patient.Patronic);
                Console.Write("Address: ");
                Output.WriteLine(ConsoleColor.Green, patient.Address);
            }
        }

        public async Task DisplayById()
        {
            Console.WriteLine("Patient by id");
            try
            {
                Console.Write("Indicate id: ");
                var id = int.Parse(Console.ReadLine());
                var patient = await _patientService.GetByIdAsync(id);

                Console.Write("First name: ");
                Output.WriteLine(ConsoleColor.Green, patient.FirstName);
                Console.Write("Surname: ");
                Output.WriteLine(ConsoleColor.Green, patient.Surname);
                Console.Write("Patronic: ");
                Output.WriteLine(ConsoleColor.Green, patient.Patronic);
                Console.Write("Address: ");
                Output.WriteLine(ConsoleColor.Green, patient.Address);
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task Create()
        {
            var patient = new Patient();

            Console.WriteLine("Create patient");
            try
            {
                Console.WriteLine("First name: ");
                patient.FirstName = Console.ReadLine();
                Console.WriteLine("Surname: ");
                patient.Surname = Console.ReadLine();
                Console.WriteLine("Patronic: ");
                patient.Patronic = Console.ReadLine();
                Console.WriteLine("Address: ");
                patient.Address = Console.ReadLine();

                await _patientService.CreateAsync(patient);
                Console.WriteLine("Patient created succesfully");
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task Update()
        {
            var patient = new Patient();

            Console.WriteLine("Update patient");
            try
            {
                Console.WriteLine("Indicate id: ");
                patient.Id = int.Parse(Console.ReadLine());
                Console.WriteLine("First name: ");
                patient.FirstName = Console.ReadLine();
                Console.WriteLine("Surname: ");
                patient.Surname = Console.ReadLine();
                Console.WriteLine("Patronic: ");
                patient.Patronic = Console.ReadLine();
                Console.WriteLine("Address: ");
                patient.Address = Console.ReadLine();

                await _patientService.UpdateAsync(patient);
                Console.WriteLine("Patient updates succesfuly");
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task Delete()
        {
            Console.WriteLine("Delete patient");
            try
            {
                Console.WriteLine("Indicate id: ");
                int id = int.Parse(Console.ReadLine());

                var allPatinets = await _patientService.GetAllAsync();
                var patient = allPatinets.Where(val => val.Id == id).FirstOrDefault();

                await _patientService.DeleteAsync(patient);
                Console.WriteLine("Patient deleted succesfully");
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
