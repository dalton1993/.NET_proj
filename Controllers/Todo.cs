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
    public class TodosController : ControllerBase
    {
        public DataContext _context {get; set;}
        public TodosController(DataContext context)
        {
            _context = context; 
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodosDTO>>> Get()
        {
            //var result =  await _context.Todos.ToListAsync();
            var result = await _context.Todos
                            .Select(todo => new TodosDTO()
                            {
                                Id = todo.Id,
                                Todo = todo.Todo,
                                Author = todo.Author
                            })
                            .ToListAsync();
                            
            if(result == null)
            {
                return BadRequest(); 
            }       

            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetTodo")]
        public async Task<ActionResult<TodosDTO>> GetItem(int id)
        {
            var todo = await _context.Todos
                        .Select(todo => new TodosDTO()
                        {
                            Id = todo.Id, 
                            Todo = todo.Todo,
                            Author = todo.Author
                        })
                        .SingleAsync(todo => todo.Id == id); 

            return Ok(todo); 
        }

        [HttpPost]
        public async Task<ActionResult<TodosDTO>> PostTodo(TodoModel todo)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var newTodo = _context.Todos.Add(todo);

            var success = await _context.SaveChangesAsync(); 

            var result = new TodosDTO()
            {
                Id = todo.Id,
                Author = todo.Author,
                Todo = todo.Todo
            };

            return CreatedAtRoute("GetTodo", new { id = todo.Id.ToString() }, result);  
        }

        [HttpDelete("/todo/delete/{id}")]
        public async Task<ActionResult<TodoModel>> Remove(int id)
        {
            var todo = await _context.Todos.SingleAsync(todo => todo.Id == id);

            _context.Todos.Remove(todo);

           await _context.SaveChangesAsync();

            return Ok("item deleted");
        }



    }
}