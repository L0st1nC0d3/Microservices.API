namespace Data
{
    public class PostRepository
    {
        private readonly List<PostDto> _posts;
        private readonly List<UserDto> _users;

        public PostRepository()
        {
            if (_posts == null)
                _posts = new List<PostDto>();
            if (_users == null)
                _users = new List<UserDto>();
        }

        public List<PostDto> GetAll(int authorId)
        {
            return _posts.Where(p => p.AuthorId.Equals(authorId)).ToList();
        }

        public List<PostDto> GetAllByUserName(string userName)
        {
            var userId = _users.FirstOrDefault(i => i.Equals(userName));
            if (userId == null)
                return new List<PostDto>();

            return _posts.Where(p => p.AuthorId.Equals(userId)).ToList();
        }

        public PostDto? GetById(int id)
        {
            return _posts.Where(p => p.Id == id).FirstOrDefault();
        }

        public int AddNewPost(int author, string title, string text)
        {
            var newPost = new PostDto
            {
                AuthorId = author,
                Id = _posts.Count + 1,
                Title = title,
                Text = text
            };

            _posts.Add(newPost);
            return newPost.Id;
        }

        public int? EditPost(int id, string newTitle, string newText)
        {
            var oldPost = _posts.Where(p => p.Id == id).FirstOrDefault();
            if (oldPost == null) return null;

            oldPost.Title = newTitle ?? oldPost.Title;
            oldPost.Text = newText ?? oldPost.Text;
            return id;
        }

        public int? DeletePost(int id)
        {
            var post = _posts.Where(p => p.Id != id).FirstOrDefault();
            if (post == null) return null;

            var removed = _posts.Remove(post);
            return removed ? id : null;
        }

        public void InsertCreatedUserId(UserDto user)
        {
            _users.Add(user);
        }

        public bool UserIdExists(int id)
        {
            return _users.Where(u => u.Id.Equals(id)).Any();
        }

        public bool UsernameExists(string userName)
        {
            return _users.Where(u => u.UserName.Equals(userName)).Any();
        }

        public void DeleteCreatedUserId(int id)
        {
            _users.RemoveAll(u => u.Id.Equals(id));
        }
    }
}