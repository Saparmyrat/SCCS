using AutoMapper;
using BusinessLayer.Entities;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.ViewModel;

namespace WebApp.Components
{
    public class GetPatients : ViewComponent
    {
        private readonly IService<Patient, int> _patientService;
        private readonly IMapper _mapper;

        public GetPatients(IService<Patient, int> patientService,
            IMapper mapper)
        {
            _patientService = patientService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var data = await _patientService.GetAllAsync();
            var result = _mapper.Map<List<PatientViewModel>>(data);

            ViewBag.Patients = new SelectList(result, "Id", "FirstName");

            return View();
        }
    }
}
