using DotNetOrchestra.Server.Services;
using DotNetOrchestra.Shared;
using Microsoft.AspNetCore.Mvc;

namespace DotNetOrchestra.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : Controller
    {
        private readonly NoteService _noteService;

        public NoteController(NoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpGet]
        public async Task<IActionResult> GetNotesAsync(CancellationToken cancellationToken)
        {
            var notes = await _noteService.GetNotesAsync(cancellationToken);
            return Ok(notes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNoteAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            var note = await _noteService.GetNoteAsync(id, cancellationToken);
            return Ok(note);
        }

        [HttpPost]
        public async Task<IActionResult> AddNoteAsync([FromBody] NoteModel note, CancellationToken cancellationToken)
        {
            var created = await _noteService.CreateNoteAsync(note, cancellationToken);
            return Ok(created);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateNoteAsync([FromBody] NoteModel note, CancellationToken cancellationToken)
        {
            var updated = await _noteService.UpdateNoteAsync(note, cancellationToken);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveNoteAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            await _noteService.RemoveNoteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
