using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotesManagement.Data;
using NotesManagement.API.Models;

namespace NotesManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController : ControllerBase
    {
        private NoteDbContext _context;

        public CategoriesController(NoteDbContext dbContext)
        {
            _context = dbContext;
        }

        [HttpGet]
        public async Task<IEnumerable<Category>> Get()
        {
            var categories = await _context.Categories.ToListAsync();

            //return empty list if no categeries have been saved yet
            return categories == null ? Enumerable.Empty<Category>() : categories;
        }

        [HttpPost]
        public async Task<Category> Post([FromBody]Category category)
        {
            if (ModelState.IsValid)
            {
                await _context.Categories.AddAsync(category);
                await _context.SaveChangesAsync();
                return category;
            }
            else
            {
                throw new Exception("Invalid Category");
            }
        }
    }
}