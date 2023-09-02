using Microsoft.AspNetCore.Mvc;
using ReadSavvy.Models;

namespace ReadSavvy.ViewModels
{
    public class EventDetailsCommentViewModel
    {
        public Event Event { get; set; }
        public Comment Comment { get; set; }
        public List<Comment> Comments { get; set; }

    }
}
