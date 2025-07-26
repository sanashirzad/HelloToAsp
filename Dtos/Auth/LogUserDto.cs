using System.ComponentModel.DataAnnotations;

namespace HelloToAsp.Dtos.Auth
{
    public class LogUserDto
    {
        [Required]
        [RegularExpression(@"^(0)?9\d{9}$",
            ErrorMessage = "شماره موبایل معتبر نیست")]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(8, MinimumLength = 4)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string PasswordConfirmation { get; set; }
    }
}
