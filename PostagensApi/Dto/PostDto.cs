namespace PostagensApi.Dto
{
    public class PostDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long AuthorId { get; set; }
        public long Likes { get; set; } 

        public List<CommentDto> Comments { get; set; } = new List<CommentDto>();
    }
}
