namespace Data
{
    public class CommentDto
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public int PostId { get; set; }
        public string Text { get; set; }
    }
}
