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
    public class UserController : ControllerBase
    {
        public DataContext _context {get;}
        public UserController(DataContext context)
        {
            _context = context; 
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllUsers()
        {
            var result = await _context.Users
                                .Select( user => new UserDTO()
                                {
                                    UserId = user.UserId,
                                    FirstName = user.FirstName,
                                    Post = user.Post,
                                    Followers = user.Followers,
                                    Following = user.Following
                                })
                                .ToListAsync();

            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetUser")]
        public async Task<ActionResult<UserDTO>> GetUser(int id)
        {
            var result = await _context.Users
                            .Select(user => new UserDTO()
                            {
                                UserId = user.UserId,
                                FirstName = user.FirstName,
                                Post = user.Post,
                                Followers = user.Followers,
                                Following = user.Following
                            })
                            .SingleAsync(user => user.UserId == id);

            return Ok(result); 
        }

        [HttpGet("{id}/communities/posts")]
        public async Task<ActionResult> GetCommunityPosts(int id)
        {
            var MyPosts = await _context.UserCommunities
                            .Where(u => u.UserId == id)
                            .Join(
                            _context.Posts,
                            c => c.CommunityId,
                            p => p.CommunityId,
                            (c, p) => new
                            {
                                CommunityId = c.CommunityId,
                                Game = c.Community.Game,
                                AuthorId = p.UserId,
                                Author = p.User.UserName,
                                PostId = p.PostId,
                                Message = p.Message,
                                Comments = p.Comments
                                    .Select(c => new 
                                        {
                                            CommentId = c.PostCommentsId, 
                                            AuthorId = c.UserId, 
                                            Author = c.User.UserName, 
                                            Comment = c.Comment
                                        })
                            }
                        )
                        .ToListAsync();
                        
            return Ok(MyPosts);
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> PostUser(User user)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Model state invalid");
            }

            _context.Users.Add(user);

            var success = await _context.SaveChangesAsync();

            Console.WriteLine(success); 

            var result = new UserDTO()
            {
                UserId = user.UserId,
                FirstName = user.FirstName
            };

            return CreatedAtRoute("GetUser", new { id = user.UserId.ToString() }, result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var user = _context.Users.SingleOrDefault(u => u.UserId == id);

            _context.Users.Remove(user);

            var updateDb = await _context.SaveChangesAsync();

            return Ok("user deleted"); 
        }
    }
}