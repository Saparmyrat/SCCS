using AutoMapper;
using BusinessLayer.Entities;
using WebApp.ViewModel;

namespace WebApp.Mapping
{
    public class WebMappingProfile : Profile
    {
        public WebMappingProfile()
        {
            CreateMap<DiseasedViewModel, Diseased>().ReverseMap();
            CreateMap<DoctorViewModel, Doctor>().ReverseMap();
            CreateMap<PatientViewModel, Patient>().ReverseMap();
            CreateMap<RecordViewModel, Record>().ReverseMap();
        }
    }
}
