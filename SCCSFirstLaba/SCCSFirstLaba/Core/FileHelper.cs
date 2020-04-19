using AutoMapper;
using SCCSFirstLaba.FileHelpers;
using SCCSFirstLaba.Interfaces;
using SCCSFirstLaba.Models;
using System.Collections.Generic;
using System.Linq;

namespace SCCSFirstLaba.Core
{
    public class FileHelper : IService
    {
        private readonly IFileHelper _fileHelper;
        private readonly IMapper _mapper;

        public FileHelper(ExcelHelper excelHelper)
        {
            _fileHelper = excelHelper;
            _mapper = new MapperConfiguration(cfg => cfg.CreateMap<Student, DataToWrite>()).CreateMapper();
        }

        public FileHelper(JsonHelper jsonHelper)
        {
            _fileHelper = jsonHelper;
            _mapper = new MapperConfiguration(cfg => cfg.CreateMap<Student, DataToWrite>()).CreateMapper();
        }

        public void CreateReport(IEnumerable<Student> item, string path)
        {
            var dataToWrite = new List<DataToWrite>();

            foreach (var student in item)
            {
                var studentToWrite = _mapper.Map<DataToWrite>(student);
                studentToWrite.AverageMarks = AverageMarks.GetAverageStudentMark(student);

                dataToWrite.Add(studentToWrite);
            }
            _fileHelper.CreateReport(dataToWrite, AverageMarks.GetAverageStudentsMark(item.ToList()), path);
        }

        public IEnumerable<Student> ReadCsvFile(string path) => _fileHelper.GetAll(path);
    }
}
