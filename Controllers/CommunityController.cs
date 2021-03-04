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
    public class CommunityController : ControllerBase
    {
        private readonly DataContext _context;
        public CommunityController(DataContext context)
        {
            _context = context; 
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreateCommunity(Community community)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("model state invalid");
            }

            _context.Communities.Add(community);

            await _context.SaveChangesAsync();

            return Ok("community has been created"); 
        }

        [HttpPost("community_user")]
        public async Task<ActionResult> JoinCommunity(UserCommunity join)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Join Model Request invalid");
            }

            var user_community = _context.UserCommunities.Add(join);

            var success = await _context.SaveChangesAsync();

            
            return Ok("community join successfull");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Community>> GetCommunity(int id)
        {
            var community = await _context.Communities
                            .Where(c => c.CommunityId == id)
                            .Join(
                                _context.Posts,
                                a => a.CommunityId,
                                b => b.CommunityId,
                                (a,b) => new 
                                {
                                    PostId = b.PostId,
                                    Post = b.Message,
                                    AuthorId = b.UserId,
                                    Author = b.User.UserName
                                }
                            )
                            .ToListAsync();

            var comm = from u in _context.UserCommunities
                        where u.CommunityId == id
                        join p in _context.Users on u.UserId equals p.UserId
                        join a in _context.Posts on u.CommunityId equals a.CommunityId
                        select new 
                        {
                            
                        };

            var result = await comm.ToListAsync();

            return Ok(result);
        }
    }
}