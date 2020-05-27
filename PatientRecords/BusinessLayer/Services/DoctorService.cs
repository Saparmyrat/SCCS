using AutoMapper;
using BusinessLayer.Entities;
using BusinessLayer.Interfaces;
using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    internal class DoctorService : IService<Doctor, int>
    {
        private readonly IRepository<DoctorDto, int> _doctorRepository;
        private readonly IMapper _mapper;

        public DoctorService(IRepository<DoctorDto, int> doctorRepository,
            IMapper mapper)
        {
            _doctorRepository = doctorRepository;
            _mapper = mapper;
        }

        public Task<Doctor> CreateAsync(Doctor item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return CreateInternalAsync(item);
        }

        public Task DeleteAsync(Doctor item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return DeleteInternalAsync(item);
        }

        private async Task DeleteInternalAsync(Doctor item)
        {
            await _doctorRepository.DeleteAsync(_mapper.Map<DoctorDto>(item));
        }

        public async Task<List<Doctor>> GetAllAsync()
        {
            var allDoctors = await _doctorRepository.GetAllAsync();
            var result = _mapper.Map<List<Doctor>>(allDoctors);

            return result;
        }

        public async Task<Doctor> GetByIdAsync(int id)
        {
            var doctor = await _doctorRepository.GetByIdAsync(id);
            var result = _mapper.Map<Doctor>(doctor);

            return result;
        }

        public Task UpdateAsync(Doctor item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return UpdateInternalAsync(item);
        }

        private async Task UpdateInternalAsync(Doctor item)
        {
            await _doctorRepository.UpdateAsync(_mapper.Map<DoctorDto>(item));
        }

        private async Task<Doctor> CreateInternalAsync(Doctor item)
        {
            await _doctorRepository.CreateAsync(_mapper.Map<DoctorDto>(item));

            var allDoctors = await _doctorRepository.GetAllAsync();
            var doctor = _mapper.Map<Doctor>(allDoctors.Last());

            return doctor;
        }
    }
}
