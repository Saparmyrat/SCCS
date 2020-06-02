using System;
using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModel
{
    public class DiseasedViewModel
    {
        public int Id { get; set; }

        public int PatientId { get; set; }

        [Display(Name = "Disease")]
        [Required(ErrorMessage = "Required disease")]
        public string Disease { get; set; }

        [Display(Name = "Date of illnes")]
        [Required(ErrorMessage = "Required date of illnes")]
        [DataType(DataType.Date)]
        public DateTimeOffset DateOfIllnes { get; set; }
    }
}
