using System.ComponentModel.DataAnnotations;

namespace PostagensApi.Requests.Post
{
    public class CreatePostRequest : Request
    {
        [Required(ErrorMessage = "Invalid Title")]
        [MaxLength(80, ErrorMessage = "The title must be 80 characters or less")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Invalid description")]
        public string description {  get; set; } = string.Empty;
    }
}
