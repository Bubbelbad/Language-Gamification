using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.UserDtos
{
    public class UserDto
    {
        [Required]
        public required string UserName { get; set; }
        [Required]
        public required string Password { get; set; }
    }
}