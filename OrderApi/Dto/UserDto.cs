using System.ComponentModel.DataAnnotations;

namespace OrderApi.Dto
{
    public class UserDto
    {
        [Required]
        public uint? Age { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? Username { get; set; }
    }
}
