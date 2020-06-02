using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModel
{
    public class PatientViewModel
    {
        public int Id { get; set; }

        [Display(Name = "First name")]
        [Required(ErrorMessage = "Required first name")]
        public string FirstName { get; set; }

        [Display(Name = "Surname")]
        [Required(ErrorMessage = "Required surname")]
        public string Surname { get; set; }

        [Display(Name = "Patronic")]
        [Required(ErrorMessage = "Required patronic")]
        public string Patronic { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Required address")]
        public string Address { get; set; }
    }
}
