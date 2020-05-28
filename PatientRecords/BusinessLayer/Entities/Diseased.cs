using System;

namespace BusinessLayer.Entities
{
    public class Diseased
    {
        public int Id { get; set; }

        public int PatientId { get; set; }

        public string Disease { get; set; }

        public DateTimeOffset DateOfIllnes { get; set; }
    }
}
