using System.ComponentModel.DataAnnotations;

namespace PostagensApi.Requests.User
{
    public class UserRegisterRequest
    {
        [Required(ErrorMessage = "A value is required")]
        public string name { get; set; } = string.Empty;

        [Required(ErrorMessage = "A value is required")]
        public string email { get; set; } = string.Empty;

        [Required(ErrorMessage = "A value is required")]
        public string password { get; set; } = string.Empty;
    }
}
