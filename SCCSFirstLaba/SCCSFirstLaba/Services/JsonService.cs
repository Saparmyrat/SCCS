using SCCSFirstLaba.Core;
using SCCSFirstLaba.FileHelpers;
using SCCSFirstLaba.Interfaces;
using SCCSFirstLaba.Models;
using System.Collections.Generic;
using System.Linq;

namespace SCCSFirstLaba.Services
{    
    public class JsonService : IService
    {
        private readonly IFileHelper _fileHelper;

        public JsonService()
        {
            _fileHelper = new JsonHelper();
        }

        public void Create(IEnumerable<Student> item, string path)
        {
            var dataToWrite = new List<DataToWrite>();

            foreach (var student in item)
            {
                var studentToWrite = new DataToWrite
                {
                    FirstName = student.FirstName,
                    Surname = student.Surname,
                    Patronymic = student.Patronymic,
                    AverageMarks = AverageMarks.GetAverageStudentMark(student),
                };

                dataToWrite.Add(studentToWrite);
            }

            _fileHelper.CreateReport(dataToWrite, AverageMarks.GetAverageStudentsMark(item.ToList()), path);
        }

        public IEnumerable<Student> ReadCsvFile(string path) => _fileHelper.GetAll(path);
    }
}
