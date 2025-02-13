using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HW1NoteKeeper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        // In-memory list to store notes (for demo purposes)
        private static List<Note> notes = new List<Note>();

        // POST: api/notes
        [HttpPost]
        public IActionResult Create([FromBody] Note note)
        {
            note.NoteId = Guid.NewGuid();  // Generate unique ID for the note
            notes.Add(note);
            return CreatedAtAction(nameof(GetById), new { id = note.NoteId }, note);
        }

        // GET: api/notes/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var note = notes.FirstOrDefault(n => n.NoteId == id);
            if (note == null)
                return NotFound();
            return Ok(note);
        }

        // PATCH: api/notes/{id}
        [HttpPatch("{id}")]
        public IActionResult Update(Guid id, [FromBody] Note updatedNote)
        {
            var note = notes.FirstOrDefault(n => n.NoteId == id);
            if (note == null)
                return NotFound();

            note.Summary = updatedNote.Summary ?? note.Summary;
            note.Details = updatedNote.Details ?? note.Details;
            return NoContent();  // 204 No Content
        }

        // DELETE: api/notes/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var note = notes.FirstOrDefault(n => n.NoteId == id);
            if (note == null)
                return NotFound();

            notes.Remove(note);
            return NoContent();  // 204 No Content
        }

        // GET: api/notes
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(notes);
        }
    }

    // Note model class
    public class Note
    {
        public Guid NoteId { get; set; }
        public string Summary { get; set; }
        public string Details { get; set; }
        public DateTime CreatedDateUtc { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedDateUtc { get; set; }
    }
}