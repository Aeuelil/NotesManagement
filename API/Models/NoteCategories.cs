using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace NotesManagement.API.Models
{
    public class NoteCategories
    {
        public int NoteId { get; set; }
        public int CategoryId { get; set; }

        [JsonIgnore]
        public Note Note { get; set; }
        public Category Category { get; set; }
    }
}