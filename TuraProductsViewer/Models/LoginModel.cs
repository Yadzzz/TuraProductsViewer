using System.ComponentModel.DataAnnotations;

namespace TuraProductsViewer.Models
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string EmailAdress { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
