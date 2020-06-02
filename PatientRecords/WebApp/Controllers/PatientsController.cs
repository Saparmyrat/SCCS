using AutoMapper;
using BusinessLayer.Entities;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.ViewModel;

namespace WebApp.Controllers
{
    public class PatientsController : Controller
    {
        private readonly IService<Patient, int> _patientService;
        private readonly IMapper _mapper;

        public PatientsController(IService<Patient, int> patientService,
            IMapper mapper)
        {
            _patientService = patientService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> ShowPatients()
        {
            var data = await _patientService.GetAllAsync();
            var result = _mapper.Map<List<PatientViewModel>>(data);

            return View(result);
        }

        [HttpGet]
        public IActionResult CreatePatient()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public Task<IActionResult> CreatePatient(PatientViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return CreatePatient(model);
            }

            return CreatePatientInternal(model);
        }

        [HttpGet]
        public async Task<IActionResult> UpdatePatient(int id)
        {
            var data = await _patientService.GetByIdAsync(id);
            var result = _mapper.Map<PatientViewModel>(data);

            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public Task<IActionResult> UpdatePatient(PatientViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return UpdatePatient(model);
            }

            return UpdatPatientInternal(model);
        }

        [HttpGet]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var data = await _patientService.GetByIdAsync(id);
            await _patientService.DeleteAsync(data);

            return RedirectToAction(nameof(ShowPatients));
        }

        private async Task<IActionResult> UpdatPatientInternal(PatientViewModel model)
        {
            var data = _mapper.Map<Patient>(model);
            await _patientService.UpdateAsync(data);

            return RedirectToAction(nameof(ShowPatients));
        }

        private async Task<IActionResult> CreatePatientInternal(PatientViewModel model)
        {
            var data = _mapper.Map<Patient>(model);
            await _patientService.CreateAsync(data);

            return RedirectToAction(nameof(ShowPatients));
        }
    }
}