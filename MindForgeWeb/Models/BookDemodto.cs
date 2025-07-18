using System.ComponentModel.DataAnnotations;

namespace MindForgeWeb.Models
{
    public class BookDemodto
    {
        public int ContactId { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(25, ErrorMessage = "Name cannot exceed 25 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Address is required.")]

        public string Address { get; set; }

        [Required(ErrorMessage = "Message is required.")]
        [MaxLength(100, ErrorMessage = "Message cannot exceed 100 characters.")]
        public string Message { get; set; }
        public string Action { get; set; }

    }
}
