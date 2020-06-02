using AutoMapper;
using BusinessLayer.Entities;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApp.ViewModel;

namespace WebApp.Components
{
    public class GetPatient : ViewComponent
    {
        private readonly IService<Patient, int> _patientService;
        private readonly IMapper _mapper;

        public GetPatient(IService<Patient, int> patientService,
            IMapper mapper)
        {
            _patientService = patientService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var data = await _patientService.GetByIdAsync(id);
            var result = _mapper.Map<PatientViewModel>(data);

            if (result != null)
            {
                ViewBag.FirstName = result.FirstName;
            }

            return View();
        }
    }
}
