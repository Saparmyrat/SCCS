using AutoMapper;
using BusinessLayer.Entities;
using BusinessLayer.Exceptions;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.ViewModel;

namespace WebApp.Controllers
{
    public class DiseasedsController : Controller
    {
        private readonly IService<Diseased, int> _diseasedService;
        private readonly IMapper _mapper;
        private readonly ILogger<DiseasedsController> _logger;

        public DiseasedsController(IService<Diseased, int> diseasedService,
            IMapper mapper,
            ILogger<DiseasedsController> logger)
        {
            _diseasedService = diseasedService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> ShowDiseaseds()
        {
            var data = await _diseasedService.GetAllAsync();
            var result = _mapper.Map<List<DiseasedViewModel>>(data);

            return View(result);
        }

        [HttpGet]
        public IActionResult CreateDiseased()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public Task<IActionResult> CreateDiseased(DiseasedViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return CreateDiseased(model);
            }

            return CreateDiseasedInternal(model);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateDiseased(int id)
        {
            var data = await _diseasedService.GetByIdAsync(id);
            var result = _mapper.Map<DiseasedViewModel>(data);

            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public Task<IActionResult> UpdateDiseased(DiseasedViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return UpdateDiseased(model);
            }

            return UpdateDiseasedInternal(model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteDiseased(int id)
        {
            var data = await _diseasedService.GetByIdAsync(id);
            await _diseasedService.DeleteAsync(data);

            return RedirectToAction(nameof(ShowDiseaseds));
        }

        private async Task<IActionResult> UpdateDiseasedInternal(DiseasedViewModel model)
        {
            try
            {
                var data = _mapper.Map<Diseased>(model);
                await _diseasedService.UpdateAsync(data);

                return RedirectToAction(nameof(ShowDiseaseds));
            }
            catch (DateException ex)
            {
                _logger.LogError(ex.Message);

                return await UpdateDiseased(model.Id);
            }
        }

        private async Task<IActionResult> CreateDiseasedInternal(DiseasedViewModel model)
        {
            try
            {
                var data = _mapper.Map<Diseased>(model);
                await _diseasedService.CreateAsync(data);

                return RedirectToAction(nameof(ShowDiseaseds));
            }
            catch (DateException ex)
            {
                _logger.LogError(ex.Message);

                return CreateDiseased();
            }
        }
    }
}