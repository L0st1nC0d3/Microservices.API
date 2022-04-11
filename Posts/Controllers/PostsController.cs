using Business;
using Business.Contracts;
using Data;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Posts.Models;

namespace Posts.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly PostsManager _manager;

        public PostsController(IPublishEndpoint publishEndpoint, PostsManager manager)
        {
            _publishEndpoint = publishEndpoint;
            _manager = manager;
        }

        [HttpPost("posts")]
        [ProducesResponseType(typeof(int), 200)]
        public async Task<ActionResult<int>> CreatePost([FromBody] Post post)
        {
            var postId = _manager.AddNewPost(post.Author, post.Title, post.Text);
            await _publishEndpoint.Publish<ICreatedPostEvent>(new
            {
                PostId = postId
            });
            return Ok(postId);
        }

        [HttpGet("posts/{userName}")]
        [ProducesResponseType(typeof(List<PostDto>), 200)]
        public ActionResult<List<PostDto>> GetPostByUserId([FromRoute] string userName)
        {
            return Ok(_manager.GetAllByUserName(userName));
        }

        [HttpGet("posts/{id:int}")]
        [ProducesResponseType(typeof(PostDto), 200)]
        public ActionResult<PostDto> GetPostById([FromRoute] int id)
        {
            return Ok(_manager.GetPostById(id));
        }

        [HttpPut("posts/{id:int}")]
        [ProducesResponseType(typeof(int), 200)]
        public ActionResult<int> EditPostById([FromRoute] int id, [FromBody] EditPost post)
        {
            return Ok(_manager.EditPost(id, post.Title, post.Text));
        }

        [HttpDelete("posts/{id:int}")]
        [ProducesResponseType(typeof(int), 200)]
        public async Task<ActionResult<int>> DeletePostById([FromRoute] int id)
        {
            var postId = _manager.DeletePost(id);
            await _publishEndpoint.Publish<IDeletedPostEvent>(new
            {
                Id = postId
            });
            return Ok(postId);
        }
    }
}