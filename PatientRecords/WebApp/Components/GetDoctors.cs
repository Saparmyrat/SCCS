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
    public class GetDoctors : ViewComponent
    {
        private readonly IService<Doctor, int> _doctorService;
        private readonly IMapper _mapper;

        public GetDoctors(IService<Doctor, int> doctorService,
            IMapper mapper)
        {
            _doctorService = doctorService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var data = await _doctorService.GetAllAsync();
            var result = _mapper.Map<List<DoctorViewModel>>(data);

            ViewBag.Doctors = new SelectList(result, "Id", "FirstName");

            return View();
        }
    }
}
