using CsvHelper;
using OfficeOpenXml;
using SCCSFirstLaba.Interfaces;
using SCCSFirstLaba.Models;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace SCCSFirstLaba.FileHelpers
{
    public class ExcelHelper : IFileHelper
    {
        public void Create(IEnumerable<DataToWrite> item, double averageGroup, string path)
        {
            const string worksheetName = "Students";
            const string averageGroupName = "Average group:";

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using var excelPackage = new ExcelPackage();

            var worksheet = excelPackage.Workbook.Worksheets.Add(worksheetName);

            worksheet.Cells["A1"].LoadFromCollection(item);
            worksheet.Cells["F1"].Value = averageGroupName;
            worksheet.Cells["F2"].Value = averageGroup;

            var fileForWrite = new FileInfo(path);
            excelPackage.SaveAs(fileForWrite);
        }

        public IEnumerable<Student> GetAll(string path)
        {
            var students = new List<Student>();
            const int numberHeadersForMarks = 3;

            try
            {
                using var reader = new StreamReader(path);
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

                csv.Read();
                csv.ReadHeader();
                var headers = csv.Context.HeaderRecord;

                while (csv.Read())
                {
                    var marks = GetMarks(headers.Length - numberHeadersForMarks, csv, headers);
                    students.Add(new Student
                    {
                        FirstName = csv.GetField(headers[0]),
                        Surname = csv.GetField(headers[1]),
                        Patronymic = csv.GetField(headers[2]),
                        Subjects = headers.Skip(numberHeadersForMarks).ToArray(),
                        Marks = marks,
                    });
                    marks = new int[headers.Length - numberHeadersForMarks];
                }
            }
            catch (MissingFieldException)
            {
                students = new List<Student>();
            }

            return students;
        }

        private int[] GetMarks(int size, CsvReader csv, string[] headers)
        {
            const int numberHeadersForMarks = 3;
            var marks = new int[size];

            for (int index = 0; index < marks.Length; index++)
            {
                if (csv.TryGetField(headers[index + numberHeadersForMarks], out marks[index]))
                {
                    marks[index] = csv.GetField<int>(headers[index + numberHeadersForMarks]);
                }
                else
                {
                    marks[index] = 0;
                }
            }

            return marks;
        }
    }
}
