using SCCSFirstLaba.Models;
using System.Collections.Generic;

namespace SCCSFirstLaba.Interfaces
{
    public interface IFileHelper
    {
        void Create(IEnumerable<DataToWrite> item, double average, string path);

        IEnumerable<Student> GetAll(string path);
    }
}
