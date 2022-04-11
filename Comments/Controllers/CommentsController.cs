using Business;
using Data;
using Microsoft.AspNetCore.Mvc;
using PostDetails;

namespace PostsDetails.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly CommentsManager _manager;

        public CommentsController(CommentsManager manager)
        {
            _manager = manager;
        }

        [HttpGet("comments/{id:int}")]
        [ProducesResponseType(typeof(CommentDto), 200)]
        public ActionResult<CommentDto> GetComments([FromRoute] int id)
        {
            return Ok(_manager.GetCommentsByPost(id));
        }

        [HttpPost("comments")]
        [ProducesResponseType(typeof(int), 200)]
        public ActionResult<Guid> CreateComment([FromBody] Comment comment)
        {
            if (comment == null)
                return BadRequest();

            if (string.IsNullOrEmpty(comment.Text))
                return BadRequest();

            return Ok(_manager.AddComment(comment.PostId, comment.AuthorId, comment.Text));
        }

        [HttpPut("comments/{id:int}")]
        [ProducesResponseType(typeof(int), 200)]
        public ActionResult<Guid> EditComment([FromRoute] int id, [FromBody] EditComment comment)
        {
            if (comment == null)
                return BadRequest();

            if (string.IsNullOrEmpty(comment.Text))
                return BadRequest();

            return Ok(_manager.EditComment(id, comment.Text));
        }

        [HttpDelete("comments/{id:int}")]
        [ProducesResponseType(typeof(int), 200)]
        public ActionResult<Guid> DeleteComment([FromRoute] int id)
        {
            return Ok(_manager.DeleteComment(id));
        } 
    }
}