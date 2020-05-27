using AutoMapper;
using BusinessLayer.Entities;
using BusinessLayer.Exceptions;
using BusinessLayer.Interfaces;
using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    internal class DiseasedService : IService<Diseased, int>
    {
        private readonly IRepository<DiseasedDto, int> _diseasedRepository;
        private readonly IMapper _mapper;

        public DiseasedService(IRepository<DiseasedDto, int> diseasedRepository,
            IMapper mapper)
        {
            _diseasedRepository = diseasedRepository;
            _mapper = mapper;
        }

        public Task<Diseased> CreateAsync(Diseased item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return CreateInternalAsync(item);
        }

        public Task DeleteAsync(Diseased item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return DeleteInternalAsync(item);
        }

        private async Task DeleteInternalAsync(Diseased item)
        {
            await _diseasedRepository.DeleteAsync(_mapper.Map<DiseasedDto>(item));
        }

        public async Task<List<Diseased>> GetAllAsync()
        {
            var allDiseaseds = await _diseasedRepository.GetAllAsync();
            var result = _mapper.Map<List<Diseased>>(allDiseaseds);

            return result;
        }

        public async Task<Diseased> GetByIdAsync(int id)
        {
            var diseased = await _diseasedRepository.GetByIdAsync(id);
            var result = _mapper.Map<Diseased>(diseased);

            return result;
        }

        public Task UpdateAsync(Diseased item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return UpdateInternalAsync(item);
        }

        private async Task UpdateInternalAsync(Diseased item)
        {
            await _diseasedRepository.UpdateAsync(_mapper.Map<DiseasedDto>(item));
        }

        private async Task<Diseased> CreateInternalAsync(Diseased item)
        {
            if (item.DateOfIllnes >= DateTimeOffset.Now)
            {
                throw new DateException("Date of illnes cannot be in the present or future tense", nameof(item));
            }

            await _diseasedRepository.CreateAsync(_mapper.Map<DiseasedDto>(item));

            var allDiseaseds = await _diseasedRepository.GetAllAsync();
            var diseased = _mapper.Map<Diseased>(allDiseaseds.Last());

            return diseased;
        }
    }
}
