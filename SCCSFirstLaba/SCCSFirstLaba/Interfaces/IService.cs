using SCCSFirstLaba.Models;
using System.Collections.Generic;

namespace SCCSFirstLaba.Interfaces
{
    public interface IService
    {
        void Create(IEnumerable<Student> item, string path);

        IEnumerable<Student> GetAll(string path);
    }
}
