using AutoMapper;
using BusinessLayer.Entities;
using DataAccessLayer.DTO;

namespace BusinessLayer.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Diseased, DiseasedDto>().ReverseMap();
            CreateMap<Doctor, DoctorDto>().ReverseMap();
            CreateMap<Patient, PatientDto>().ReverseMap();
            CreateMap<Record, RecordDto>().ReverseMap();
        }
    }
}
