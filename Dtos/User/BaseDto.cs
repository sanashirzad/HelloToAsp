using System.ComponentModel.DataAnnotations;

namespace HelloToAsp.Dtos.User
{
    public abstract class BaseDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
}
