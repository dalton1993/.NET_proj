using System.Threading.Tasks;
using Context;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RelationshipController : ControllerBase
    {
        public DataContext _context {get;}
        public RelationshipController(DataContext context)
        {
            _context = context; 
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> Follow(UserToUser data)
        {

            if(!ModelState.IsValid)
            {
                return BadRequest("Model state invalid");
            }

            _context.Relationships.Add(data);

            await _context.SaveChangesAsync();

            return Ok("relationship saved");
        }

        
    }
}