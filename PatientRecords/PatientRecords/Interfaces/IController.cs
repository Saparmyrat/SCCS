using System.Threading.Tasks;

namespace PatientRecords.Interfaces
{
    public interface IController
    {
        void Start();

        Task DisplayAll();

        Task DisplayById();

        Task Create();

        Task Update();

        Task Delete();
    }
}
