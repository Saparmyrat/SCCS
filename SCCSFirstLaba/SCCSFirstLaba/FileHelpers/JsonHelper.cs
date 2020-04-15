using Newtonsoft.Json;
using SCCSFirstLaba.Interfaces;
using SCCSFirstLaba.Models;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SCCSFirstLaba.FileHelpers
{
    public class JsonHelper : IFileHelper
    {
        public void Create(IEnumerable<DataToWrite> item, double averageGroup, string path)
        {
            using var writer = new StreamWriter(path, false, Encoding.UTF8);
            writer.Write(JsonConvert.SerializeObject(item));
        }

        public IEnumerable<Student> GetAll(string path) => JsonConvert.DeserializeObject<IEnumerable<Student>>(File.ReadAllText(path));
    }
}
