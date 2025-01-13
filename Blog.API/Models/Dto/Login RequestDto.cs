using System.ComponentModel.DataAnnotations;

namespace Blog.API.Models.Dto
{
    public class Login_RequestDto
    {
        [Required(ErrorMessage = "It has to be email,Required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "It has to be password,Required")]
        public string Password { get; set; }
    }
}
