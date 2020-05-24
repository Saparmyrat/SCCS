using System;

namespace DataAccessLayer.DTO
{
    public class DiseasedDto
    {
        public int Id { get; set; }

        public int PatientId { get; set; }

        public string Disease { get; set; }

        public DateTimeOffset DateOfIllnes { get; set; }
    }
}
