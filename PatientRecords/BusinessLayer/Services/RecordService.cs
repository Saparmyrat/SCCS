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
    internal class RecordService : IService<Record, int>
    {
        private readonly IRepository<RecordDto, int> _recordRepository;
        private readonly IMapper _mapper;

        public RecordService(IRepository<RecordDto, int> recordRepository,
            IMapper mapper)
        {
            _recordRepository = recordRepository;
            _mapper = mapper;
        }

        public Task<Record> CreateAsync(Record item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return CreateInternalAsync(item);
        }

        public Task DeleteAsync(Record item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return DeleteInternalAsync(item);
        }

        private async Task DeleteInternalAsync(Record item)
        {
            await _recordRepository.DeleteAsync(_mapper.Map<RecordDto>(item));
        }

        public async Task<List<Record>> GetAllAsync()
        {
            var allRecords = await _recordRepository.GetAllAsync();
            var record = _mapper.Map<List<Record>>(allRecords);

            return record;
        }

        public async Task<Record> GetByIdAsync(int id)
        {
            var record = await _recordRepository.GetByIdAsync(id);
            var result = _mapper.Map<Record>(record);

            return result;
        }

        public Task UpdateAsync(Record item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return UpdateInternalAsync(item);
        }

        private async Task UpdateInternalAsync(Record item)
        {
            if (item.Date < DateTimeOffset.Now)
            {
                throw new DateException("Date of disease cannot be in the past", nameof(item));
            }

            await _recordRepository.UpdateAsync(_mapper.Map<RecordDto>(item));
        }

        private async Task<Record> CreateInternalAsync(Record item)
        {
            if (item.Date < DateTimeOffset.Now)
            {
                throw new DateException("Date of disease cannot be in the past", nameof(item));
            }

            await _recordRepository.CreateAsync(_mapper.Map<RecordDto>(item));

            var allRecords = await _recordRepository.GetAllAsync();
            var record = _mapper.Map<Record>(allRecords.Last());

            return record;
        }
    }
}
