namespace PatientRecords.Interfaces
{
    public interface IController
    {
        void Start();

        void DisplayAll();

        void DisplayById();

        void Create();

        void Update();

        void Delete();
    }
}
