using System;

namespace BusinessLayer.Entities
{
    public class Record
    {
        public int Id { get; set; }

        public int DoctorId { get; set; }

        public int PatientId { get; set; }

        public int Cabinet { get; set; }

        public DateTimeOffset Date { get; set; }
    }
}
