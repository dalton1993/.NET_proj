using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Context;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostsController : ControllerBase
    {
        public DataContext _context {get;}
        public PostsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostsDTO>>> GetAllPosts()
        {
            var results = await _context.Posts.Include(p => p.Comments).ThenInclude(c => c.Replies)
                            .Select(posts => new PostsDTO()
                            {
                                PostId = posts.PostId,
                                Message = posts.Message,
                                CreatedAt = posts.CreatedAt,
                                Comments = posts.Comments
                            })
                            .ToListAsync();

            return Ok(results);
        }

        [HttpPost]
        public async Task<ActionResult<PostsDTO>> GetAllPosts(Post post)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("model state is not valid");
            }

            _context.Add(post); 

            var success = await _context.SaveChangesAsync();

            var result = new PostsDTO()
            {
                PostId = post.PostId,
                Message = post.Message,
                CreatedAt = post.CreatedAt
            };

            return Ok("post successfully created");
        }

        //post comments controllers

        [HttpPost("comment/add")]
        public async Task<ActionResult<PostsDTO>> Comment(PostComments comment)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Model state invalid");
            }

            var post = _context.Comments.Add(comment);

            var success = await _context.SaveChangesAsync();

            return Ok("Comment Successfully added");
        }

        [HttpDelete("comment/delete/{id}")]
        public async Task<ActionResult<PostsDTO>> DeletePosts(int id)
        {
            var comment =  _context.Comments
                            .SingleOrDefault(comment => comment.PostCommentsId == id);

            Console.WriteLine(comment); 

            _context.Comments.Remove(comment);

            var success = await _context.SaveChangesAsync();

            return Ok("comment successfully deleted"); 
        }

        //post comment replies

        [HttpPost("comment/reply")]
        public async Task<ActionResult<PostsDTO>> CommentReply(CommentReply reply)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            _context.Replies.Add(reply);

            var success = await _context.SaveChangesAsync();

            return Ok("reply successfully saved"); 
        }

        [HttpDelete("reply/delete/{id}")]
        public async Task<ActionResult> DeleteReply(int id)
        {
            var reply = _context.Replies.SingleOrDefault(r => r.CommentReplyId == id);

            var deleteReply = _context.Replies.Remove(reply);

            var DbSave = await _context.SaveChangesAsync();

            return Ok("reply successfully deleted");
        }
    }
}