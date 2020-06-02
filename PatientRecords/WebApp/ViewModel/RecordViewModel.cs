using System;
using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModel
{
    public class RecordViewModel
    {
        public int Id { get; set; }

        public int DoctorId { get; set; }

        public int PatientId { get; set; }

        [Display(Name = "Cabinet")]
        [Required(ErrorMessage = "Required cabinet")]
        public int Cabinet { get; set; }

        [Display(Name = "Date")]
        [Required(ErrorMessage = "Required date")]
        [DataType(DataType.Date)]
        public DateTimeOffset Date { get; set; }
    }
}
