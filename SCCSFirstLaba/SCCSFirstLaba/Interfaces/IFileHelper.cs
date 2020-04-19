using SCCSFirstLaba.Models;
using System.Collections.Generic;

namespace SCCSFirstLaba.Interfaces
{
    public interface IFileHelper
    {
        void CreateReport(IEnumerable<DataToWrite> item, double average, string path);

        IEnumerable<Student> GetAll(string path);
    }
}
