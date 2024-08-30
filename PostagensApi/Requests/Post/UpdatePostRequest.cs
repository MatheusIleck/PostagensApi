using System.ComponentModel.DataAnnotations;

namespace PostagensApi.Requests.Post
{
    public class UpdatePostRequest : Request
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Título inválido")]
        [MaxLength(80, ErrorMessage = "O título deve conter até 80 caracteres")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Descrição inválida")]
        public string? Description { get; set; }
    }
}
