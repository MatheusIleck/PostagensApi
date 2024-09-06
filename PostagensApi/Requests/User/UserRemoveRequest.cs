using System.ComponentModel.DataAnnotations;

namespace PostagensApi.Requests.User
{
    public class UserRemoveRequest
    {
        [Required(ErrorMessage = "A value is required")]
        public long id { get; set; }
    }
}
