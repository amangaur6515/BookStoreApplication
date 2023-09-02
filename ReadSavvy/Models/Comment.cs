using System.ComponentModel.DataAnnotations;

namespace ReadSavvy.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public int EventId { get; set; }
        public string CommentText { get; set; }
        public string Author { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
