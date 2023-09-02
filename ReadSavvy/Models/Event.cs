using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReadSavvy.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Location { get; set; }

        public string StartTime { get; set; }

        public string Type { get; set; }

        public int Duration { get; set; }

        public string Description { get; set; }
        public string OtherDetails { get; set; }

        public string InviteByEmail { get; set; }
        public string CreatedBy { get; set; }



    }
}
