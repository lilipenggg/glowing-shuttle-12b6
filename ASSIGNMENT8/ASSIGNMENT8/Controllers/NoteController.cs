using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ASSIGNMENT8.Models;
using System.Linq;

namespace ASSIGNMENT8.Controllers
{
    [Route("api/[controller]")]
    public class NoteController : Controller
    {
        [HttpGet("")]
        public IEnumerable<Note> List(string username)
        {
            return new List<Note>
            {
                new Note {Title = "Shopping list", Contents = "Some Apples"},
                new Note {Title = "Thoughts on .NET Core", Contents = "To follow..."}
            };
        }

        [HttpGet("{id}")]
        public Note Details(int id)
        {
            return new Note {Title = "one note", Contents = $"here's a note whose id is...{id}"};
        }

    }
}