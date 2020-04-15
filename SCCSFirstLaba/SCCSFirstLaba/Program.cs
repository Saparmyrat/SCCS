using CommandLine;
using SCCSFirstLaba.CommandLineOptions;
using SCCSFirstLaba.Models;
using SCCSFirstLaba.Services;
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
            var excelService = new ExcelService();
            excelService.Create(students, path);
        }
        
        private static void SaveJson(IEnumerable<Student> students, string path)
        {
            var jsonService = new JsonService();
            jsonService.Create(students, path);
        }
        
        private static IEnumerable<Student> GetData(string path)
        {
            var excelService = new ExcelService();
            var students = excelService.GetAll(path);

            return students;
        }
    }
}
