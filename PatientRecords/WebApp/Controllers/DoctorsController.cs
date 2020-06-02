using AutoMapper;
using BusinessLayer.Entities;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.ViewModel;

namespace WebApp.Controllers
{
    public class DoctorsController : Controller
    {
        private readonly IService<Doctor, int> _doctorService;
        private readonly IMapper _mapper;

        public DoctorsController(IService<Doctor, int> doctorService,
            IMapper mapper)
        {
            _doctorService = doctorService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> ShowDoctors()
        {
            var data = await _doctorService.GetAllAsync();
            var result = _mapper.Map<List<DoctorViewModel>>(data);

            return View(result);
        }

        [HttpGet]
        public IActionResult CreateDoctor()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public Task<IActionResult> CreateDoctor(DoctorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return CreateDoctor(model);
            }

            return CreateDoctorInternal(model);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateDoctor(int id)
        {
            var data = await _doctorService.GetByIdAsync(id);
            var result = _mapper.Map<DoctorViewModel>(data);

            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public Task<IActionResult> UpdateDoctor(DoctorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return UpdateDoctor(model);
            }

            return UpdateDoctorInternal(model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            var data = await _doctorService.GetByIdAsync(id);
            await _doctorService.DeleteAsync(data);

            return RedirectToAction(nameof(ShowDoctors));
        }

        private async Task<IActionResult> UpdateDoctorInternal(DoctorViewModel model)
        {
            var data = _mapper.Map<Doctor>(model);
            await _doctorService.UpdateAsync(data);

            return RedirectToAction(nameof(ShowDoctors));
        }

        private async Task<IActionResult> CreateDoctorInternal(DoctorViewModel model)
        {
            var data = _mapper.Map<Doctor>(model);
            await _doctorService.CreateAsync(data);

            return RedirectToAction(nameof(ShowDoctors));
        }
    }
}