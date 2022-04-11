namespace PostDetails
{
    public class Comment : EditComment
    {
        public int AuthorId { get; set; }
        public int PostId { get; set; }
    }

    public class EditComment
    {
        public string Text { get; set; }
    }
}
