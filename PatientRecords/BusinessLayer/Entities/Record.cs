using System;

namespace BusinessLayer.Entities
{
    public class Record
    {
        public int Id { get; set; }

        public int PatientId { get; set; }

        public string Disease { get; set; }

        public DateTimeOffset DateOfDisease { get; set; }
    }
}
