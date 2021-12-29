using System.ComponentModel.DataAnnotations;

namespace klient.Models
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "User name admin/user")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "User password 123")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember you?")]
        public bool RememberLogin { get; set; }

        public string ReturnUrl { get; set; }

    }
}

