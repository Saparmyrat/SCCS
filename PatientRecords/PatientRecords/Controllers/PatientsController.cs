using BusinessLayer.Entities;
using BusinessLayer.Interfaces;
using EasyConsole;
using PatientRecords.Interfaces;
using System;
using System.Linq;

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
              .Add("Display all patients", () => DisplayAll())
              .Add("Display patient by id", () => DisplayById())
              .Add("Create patient", () => Create())
              .Add("Update patient", () => Update())
              .Add("Delete patient", () => Delete());

            menu.Display();
        }

        public void DisplayAll()
        {
            var allPatients = _patientService.GetAllAsync().GetAwaiter().GetResult();

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

        public void DisplayById()
        {
            Console.WriteLine("Patient by id");
            try
            {
                Console.Write("Indicate id: ");
                var id = int.Parse(Console.ReadLine());
                var patient = _patientService.GetByIdAsync(id).GetAwaiter().GetResult();

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

        public void Create()
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

                _patientService.CreateAsync(patient).GetAwaiter().GetResult();
                Console.WriteLine("Patient created succesfully");
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Update()
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

                _patientService.UpdateAsync(patient).GetAwaiter().GetResult();
                Console.WriteLine("Patient updates succesfuly");
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Delete()
        {
            Console.WriteLine("Delete patient");
            try
            {
                Console.WriteLine("Indicate id: ");
                int id = int.Parse(Console.ReadLine());

                var allPatinets = _patientService.GetAllAsync().GetAwaiter().GetResult();
                var patient = allPatinets.Where(val => val.Id == id).FirstOrDefault();

                _patientService.DeleteAsync(patient).GetAwaiter().GetResult();
                Console.WriteLine("Patient deleted succesfully");
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
