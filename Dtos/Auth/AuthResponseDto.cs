using System.ComponentModel.DataAnnotations;

namespace HelloToAsp.Dtos.Auth
{
    public class AuthResponseDto
    {
        [Required]
        public string Token { get; set; }

        [Required]
        public string RefreshToken { get; set; }
    }
}
