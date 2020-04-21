using CommandLine;
using SCCSFirstLaba.CommandLineOptions;
using SCCSFirstLaba.Core;
using SCCSFirstLaba.FileHelpers;
using SCCSFirstLaba.Models;
using System.Collections.Generic;

namespace SCCSFirstLaba
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            const string excelType = "Excel";
            const string jsonType = "Json";
            Parser.Default.ParseArguments<Options>(args)
                   .WithParsed(o =>
                   {
                       string path = o.InputFile;
                       var students = GetData(path);

                       if (o.FileType == excelType)
                       {
                           path = o.OutputFile + ".xlsx";
                           SaveExcel(students, path);
                       }
                       else if (o.FileType == jsonType)
                       {
                           path = o.OutputFile + ".json";
                           SaveJson(students, path);
                       }
                   });
        }
        
        private static void SaveExcel(IEnumerable<Student> students, string path)
        {
            var excelHelper = new FileHelper(new ExcelHelper());
            excelHelper.CreateReport(students, path);
        }
        
        private static void SaveJson(IEnumerable<Student> students, string path)
        {
            var jsonHelper = new FileHelper(new JsonHelper());
            jsonHelper.CreateReport(students, path);
        }
        
        private static IEnumerable<Student> GetData(string path)
        {
            var excelHelper = new FileHelper(new ExcelHelper());
            var students = excelHelper.ReadCsvFile(path);

            return students;
        }
    }
}
