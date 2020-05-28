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
    internal class PatientService : IService<Patient, int>
    {
        private readonly IRepository<PatientDto, int> _patientRepository;
        private readonly IMapper _mapper;

        public PatientService(IRepository<PatientDto, int> patientRepository,
            IMapper mapper)
        {
            _patientRepository = patientRepository;
            _mapper = mapper;
        }

        public Task<Patient> CreateAsync(Patient item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return CreateInternalAsync(item);
        }

        public Task DeleteAsync(Patient item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return DeleteInternalAsync(item);
        }

        private async Task DeleteInternalAsync(Patient item)
        {
            await _patientRepository.DeleteAsync(_mapper.Map<PatientDto>(item));
        }

        public async Task<List<Patient>> GetAllAsync()
        {
            var allPatines = await _patientRepository.GetAllAsync();
            var patient = _mapper.Map<List<Patient>>(allPatines);

            return patient;
        }

        public async Task<Patient> GetByIdAsync(int id)
        {
            var patient = await _patientRepository.GetByIdAsync(id);
            var result = _mapper.Map<Patient>(patient);

            return result;
        }

        public Task UpdateAsync(Patient item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return UpdateInternalAsync(item);
        }

        private async Task UpdateInternalAsync(Patient item)
        {
            await _patientRepository.UpdateAsync(_mapper.Map<PatientDto>(item));
        }

        private async Task<Patient> CreateInternalAsync(Patient item)
        {
            await _patientRepository.CreateAsync(_mapper.Map<PatientDto>(item));

            var allPatients = await _patientRepository.GetAllAsync();
            var patient = _mapper.Map<Patient>(allPatients.Last());

            return patient;
        }
    }
}
