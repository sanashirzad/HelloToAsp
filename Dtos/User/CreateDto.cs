using System.ComponentModel.DataAnnotations;

namespace HelloToAsp.Dtos.User
{
    public class CreateDto : BaseDto
    {
        [Required]
        [StringLength(8, MinimumLength = 4)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string PasswordConfirmation { get; set; }
        public string UserName => PhoneNumber;
        public string NormalizedUserName => PhoneNumber;
    }
}
