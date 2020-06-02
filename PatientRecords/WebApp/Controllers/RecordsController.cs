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
    public class RecordsController : Controller
    {
        private readonly IService<Record, int> _recordService;
        private readonly IMapper _mapper;
        private readonly ILogger<RecordsController> _logger;

        public RecordsController(IService<Record, int> recordService,
            IMapper mapper,
            ILogger<RecordsController> logger)
        {
            _recordService = recordService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> ShowRecords()
        {
            var data = await _recordService.GetAllAsync();
            var result = _mapper.Map<List<RecordViewModel>>(data);

            return View(result);
        }

        [HttpGet]
        public IActionResult CreateRecord()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public Task<IActionResult> CreateRecord(RecordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return CreateRecord(model);
            }

            return CreateRecordInternal(model);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateRecord(int id)
        {
            var data = await _recordService.GetByIdAsync(id);
            var result = _mapper.Map<RecordViewModel>(data);

            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public Task<IActionResult> UpdateRecord(RecordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return UpdateRecord(model);
            }

            return UpdateRecordInternal(model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteRecord(int id)
        {
            var data = await _recordService.GetByIdAsync(id);
            await _recordService.DeleteAsync(data);

            return RedirectToAction(nameof(ShowRecords));
        }

        private async Task<IActionResult> UpdateRecordInternal(RecordViewModel model)
        {
            try
            {
                var data = _mapper.Map<Record>(model);
                await _recordService.UpdateAsync(data);

                return RedirectToAction(nameof(ShowRecords));
            }
            catch (DateException ex)
            {
                _logger.LogError(ex.Message);

                return await UpdateRecord(model.Id);
            }
        }

        private async Task<IActionResult> CreateRecordInternal(RecordViewModel model)
        {
            try
            {
                var data = _mapper.Map<Record>(model);
                await _recordService.CreateAsync(data);

                return RedirectToAction(nameof(ShowRecords));
            }
            catch (DateException ex)
            {
                _logger.LogError(ex.Message);

                return CreateRecord();
            }
        }
    }
}