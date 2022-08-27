using System.ComponentModel.DataAnnotations;

namespace NotesManagement.API.Models
{
    public class Note
    {
        [Key]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        [Required]
        public string Body { get; set; }

        public ICollection<NoteCategories> NoteCategories { get; set; }

    }
}