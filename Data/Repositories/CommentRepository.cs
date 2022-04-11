namespace Data
{
    public class CommentRepository
    {
        private readonly List<CommentDto> _comments;
        private readonly List<UserDto> _userIds;
        private readonly List<int> _postIds;

        public CommentRepository()
        {
            if (_comments == null)
                _comments = new List<CommentDto>();
            if (_userIds == null)
                _userIds = new List<UserDto>();
            if (_postIds == null)
                _postIds = new List<int>();
        }

        public int? AddComment(int post, int author, string text)
        {
            var newComment = new CommentDto
            {
                AuthorId = author,
                Id = _comments.Count + 1,
                PostId = post,
                Text = text
            };

            _comments.Add(newComment);
            return newComment.Id;
        }

        public List<CommentDto> GetCommentsByPostId(int postId)
        {
            return _comments.Where(c => c.PostId == postId).ToList();
        }

        public CommentDto? GetCommentById(int id)
        {
            return _comments.FirstOrDefault(c => c.Id == id);
        }

        public int? EditComment(int id, string newText)
        {
            var oldComment = _comments.FirstOrDefault(x => x.Id == id);
            if (oldComment == null) return null;

            oldComment.Text = newText;
            return id;
        }

        public int? DeleteComment(int id)
        {
            var comment = _comments.FirstOrDefault(x => x.Id == id);
            if (comment == null) return null;

            var removed = _comments.Remove(comment);
            return removed ? id : null;
        }

        public bool UserIdExists(int id)
        {
            return _userIds.FindAll(x => x.Id.Equals(id)).Any();
        }

        public bool PostIdExists(int id)
        {
            return _postIds.FindAll(x => x.Equals(id)).Any();
        }

        public void InsertCreatedUser(UserDto user)
        {
            _userIds.Add(user);
        }

        public void DeleteCreatedUserId(int id)
        {
            _userIds.RemoveAll(u => u.Id.Equals(id));
        }

        public void InsertCreatedPostId(int id)
        {
            _postIds.Add(id);
        }

        public void DeleteCreatedPostId(int id)
        {
            _postIds.RemoveAll(p => p == id);
        }
    }
}
