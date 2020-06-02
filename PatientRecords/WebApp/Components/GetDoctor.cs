using AutoMapper;
using BusinessLayer.Entities;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApp.ViewModel;

namespace WebApp.Components
{
    public class GetDoctor : ViewComponent
    {
        private readonly IService<Doctor, int> _doctorService;
        private readonly IMapper _mapper;

        public GetDoctor(IService<Doctor, int> doctorService,
            IMapper mapper)
        {
            _doctorService = doctorService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var data = await _doctorService.GetByIdAsync(id);
            var result = _mapper.Map<DoctorViewModel>(data);

            if (result != null)
            {
                ViewBag.FirstName = result.FirstName;
            }

            return View();
        }
    }
}
