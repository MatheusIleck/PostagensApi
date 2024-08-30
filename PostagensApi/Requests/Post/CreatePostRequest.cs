using System.ComponentModel.DataAnnotations;

namespace PostagensApi.Requests.Post
{
    public class CreatePostRequest : Request
    {
        [Required(ErrorMessage = "Título Inválido")]
        [MaxLength(80, ErrorMessage = "O titulo deve conter ate 80 caracteres")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Descrição Inválida")]
        public string description {  get; set; } = string.Empty;
    }
}
