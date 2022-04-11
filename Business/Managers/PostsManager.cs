using Business.Contracts;
using Data;
using MassTransit;

namespace Business
{
    public class PostsManager : IConsumer<ICreatedUserEvent>, IConsumer<IDeletedUserEvent>
    {
        private readonly PostRepository _postRepository;

        public PostsManager(PostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public int AddNewPost(int author, string title, string text)
        {
            if (!_postRepository.UserIdExists(author))
                throw new Exception("Errore: utente non esistente");

            return _postRepository.AddNewPost(author, title, text);
        }

        public int EditPost(int id, string newTitle, string newText)
        {
            var result = _postRepository.EditPost(id, newTitle, newText);
            if (!result.HasValue)
                throw new Exception("Errore nella modifica");

            return result.Value;
        }

        public List<PostDto> GetAllByUserName(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                return new List<PostDto>();

            if (!_postRepository.UsernameExists(userName))
                throw new Exception("Errore: utente non esistente");

            var posts = _postRepository.GetAllByUserName(userName);

            return posts ?? new List<PostDto>();
        }

        public List<PostDto> GetAllByUserId(int userId)
        {
            if (userId <= 0)
                return new List<PostDto>();

            if (!_postRepository.UserIdExists(userId))
                throw new Exception("Errore: utente non esistente");

            var posts = _postRepository.GetAll(userId);

            return posts ?? new List<PostDto>();
        }

        public PostDto GetPostById(int id)
        {
            if (id <= 0)
                return new PostDto();

            var post = _postRepository.GetById(id);

            return post ?? new PostDto();
        }

        public int DeletePost(int id)
        {
            if (id <= 0)
                throw new Exception("Id non valido");

            var result = _postRepository.DeletePost(id);
            if (!result.HasValue)
                throw new Exception("Errore nella cancellazione");

            return result.Value;
        }

        public Task Consume(ConsumeContext<ICreatedUserEvent> context)
        {
            _postRepository.InsertCreatedUserId(new UserDto
            {
                Id = context.Message.UserId,
                UserName = context.Message.UserName
            });
            return Task.CompletedTask;
        }

        public Task Consume(ConsumeContext<IDeletedUserEvent> context)
        {
            _postRepository.DeleteCreatedUserId(context.Message.Id);
            return Task.CompletedTask;
        }
    }
}
