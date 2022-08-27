using Markdig;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotesManagement.Data;
using NotesManagement.API.Models;

namespace NotesManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotesController : ControllerBase
    {
        private NoteDbContext _context;

        public NotesController(NoteDbContext dbContext)
        {
            _context = dbContext;
        }

        [HttpGet]
        public async Task<IEnumerable<Note>> Get()
        {
            var notes = await _context.Notes
                                .Include(p => p.NoteCategories)
                                .ThenInclude(c => c.Category)
                                .AsNoTracking()
                                .ToListAsync();

            // Return empty list if no notes have been saved yet
            return notes == null ? Enumerable.Empty<Note>() : notes;
        }

        [HttpGet("{id}")]
        public async Task<Note> Get(int id)
        {
            var note = await _context.Notes
                                .Include(p => p.NoteCategories)
                                .ThenInclude(c => c.Category)
                                .AsNoTracking()
                                .FirstOrDefaultAsync(a => a.Id == id);

            // Return empty note if null, else return note.
            return note == null ? new Note() : note;
        }

        [HttpGet("{id}/html")]
        public async Task<Note> GetHtml(int id)
        {
            var note = await _context.Notes
                                .Include(p => p.NoteCategories)
                                .ThenInclude(c => c.Category)
                                .AsNoTracking()
                                .FirstOrDefaultAsync(a => a.Id == id);

            if(note != null)
            {
                // Transform any markup text to HTML.
                note.Body = Markdown.ToHtml(note.Body);
            }
            
            // Return empty note if null, else return note.
            return note == null ? new Note() : note;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Note note)
        {
            string badResult;

            try
            {
                if (note != null)
                {
                    if (ModelState.IsValid)
                    {
                        note.Date = DateTime.Now;
                        await _context.Notes.AddAsync(note);
                        await _context.SaveChangesAsync();

                        return Ok(note);
                    }
                    else
                    {
                        badResult = "Invalid Note";
                    }
                }
                else
                {
                    badResult = "Empty Note";
                }
            }
            catch (DbUpdateException)
            {
                badResult = "Error with saving";
            }
            catch (Exception ex)
            {
                badResult = ex.Message;
            }

            //If something has failed, return the resulting text.
            return BadRequest(badResult);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Note details)
        {
            var note = await _context.Notes
                                .Include(p => p.NoteCategories)
                                .FirstOrDefaultAsync(a => a.Id == id);

            if (note != null)
            {
                note.Date = DateTime.Now;
                note.Body = details.Body;

                // Only update categories if some are passed through
                if(details.NoteCategories != null)
                {
                    note.NoteCategories.Clear();
                    note.NoteCategories = details.NoteCategories;
                }

                await _context.SaveChangesAsync();
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var note = await _context.Notes.FindAsync(id);

            // Remove note if found.
            if (note != null)
            {
                _context.Notes.Remove(note);
                await _context.SaveChangesAsync();
            }
            
            return Ok();
        }
    }
}