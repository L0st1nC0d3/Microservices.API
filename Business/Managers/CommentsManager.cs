using Business.Contracts;
using Data;
using MassTransit;

namespace Business
{
    public class CommentsManager : IConsumer<ICreatedUserEvent>, IConsumer<ICreatedPostEvent>, IConsumer<IDeletedUserEvent>, IConsumer<IDeletedPostEvent>
    {
        private readonly CommentRepository _commentsRepository;

        public CommentsManager(CommentRepository commentsRepository)
        {
            _commentsRepository = commentsRepository;
        }

        public int AddComment(int postId, int authorId, string text)
        {
            if (!_commentsRepository.PostIdExists(postId))
                throw new Exception("PostId non esistente");
            if (!_commentsRepository.UserIdExists(authorId))
                throw new Exception("AuthorId non esistente");

            var result = _commentsRepository.AddComment(postId, authorId, text);
            if (!result.HasValue)
                throw new Exception("Errore nella creazione del commento");

            return result.Value;
        }

        public List<CommentDto> GetCommentsByPost(int postId)
        {
            return _commentsRepository.GetCommentsByPostId(postId);
        }

        public int EditComment(int id, string text)
        {
            var oldComment = _commentsRepository.GetCommentById(id);
            if (oldComment == null)
                throw new Exception("Commento non esistente");

            var result = _commentsRepository.EditComment(id, text);
            if (!result.HasValue)
                throw new Exception("Errore nella modifica del commento");

            return result.Value;
        }

        public int DeleteComment(int id)
        {
            var result = _commentsRepository.DeleteComment(id);
            if (!result.HasValue)
                throw new Exception("Errore nella cancellazione del commento");

            return result.Value;
        }

        public Task Consume(ConsumeContext<ICreatedUserEvent> context)
        {
            _commentsRepository.InsertCreatedUser(new UserDto
            {
                Id = context.Message.UserId,
                UserName = context.Message.UserName
            });
            return Task.CompletedTask;
        }

        public Task Consume(ConsumeContext<ICreatedPostEvent> context)
        {
            _commentsRepository.InsertCreatedPostId(context.Message.PostId);
            return Task.CompletedTask;
        }

        public Task Consume(ConsumeContext<IDeletedUserEvent> context)
        {
            _commentsRepository.DeleteCreatedUserId(context.Message.Id);
            return Task.CompletedTask;
        }

        public Task Consume(ConsumeContext<IDeletedPostEvent> context)
        {
            _commentsRepository.DeleteCreatedPostId(context.Message.Id);
            return Task.CompletedTask;
        }
    }
}
