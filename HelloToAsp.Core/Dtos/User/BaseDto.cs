using System.ComponentModel.DataAnnotations;

namespace HelloToAsp.Core.Dtos.User
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
        [RegularExpression(@"^(0)?9\d{9}$",
            ErrorMessage = "شماره موبایل معتبر نیست")]
        public string PhoneNumber { get; set; }
    }
}
