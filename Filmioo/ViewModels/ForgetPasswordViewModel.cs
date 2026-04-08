using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
    public class ForgetPasswordViewModel
    {
        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string EmailAddress { get; set; }
    }
}
