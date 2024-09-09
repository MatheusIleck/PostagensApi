using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PostagensApi.Requests.Post
{
    public class UpdatePostRequest 
    {

        [JsonIgnore]
        public long Id { get; set; }

        [Required(ErrorMessage = "Invalid Title")]
        [MaxLength(80, ErrorMessage = "The title must be 80 characters or less")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Invalid description")]
        public string? Description { get; set; }

        [JsonIgnore]
        public long UserId { get; set; }

    }
}
