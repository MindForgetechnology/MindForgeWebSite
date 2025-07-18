using System.ComponentModel.DataAnnotations;

namespace MindForgeWeb.Models
{
    public class LoginDto
    {

        //[Required(ErrorMessage = "Name is required.")]
        //[StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        //public string UserName { get; set; }

        [Required(ErrorMessage = "User Id is required.")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

    }
}
