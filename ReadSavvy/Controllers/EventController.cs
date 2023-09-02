using ReadSavvy.Data;
using ReadSavvy.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;
using ReadSavvy.ViewModels;
using ReadSavvy.Services;

namespace ReadSavvy.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        //private  LogService _logService;
        
            
        
        public EventController(ApplicationDbContext db, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        
        public IActionResult Index()
        {
            IEnumerable<Event> eventList = _db.Events.ToList();

            return View(eventList);
        }
       
        public IActionResult AddEvent()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public IActionResult AddEvent(Event obj)
        {
            if (ModelState.IsValid)
            {
                _db.Events.Add(obj);
                _db.SaveChanges();
                //after form submission go to index page of this controller
                TempData["Success"] = "Event Created Sucessfully";
                return RedirectToAction("Index");
            }
            return View(obj);

        }
        
        public IActionResult EditEvent(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            Event eventFromDb = _db.Events.FirstOrDefault(e => e.Id == Id);
            //Event eventFromDb= _db.Events.Find(Id);
            if (eventFromDb == null)
            {
                return NotFound();
            }
            return View(eventFromDb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public IActionResult EditEvent(Event obj)
        {
            if (ModelState.IsValid)
            {
                _db.Events.Update(obj);
                _db.SaveChanges();
                TempData["Success"] = "Event Edited Sucessfully";
                // Redirect to the index page of this controller
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        [Route("User/event-details")]
        public IActionResult EventDetails(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            Event obj = _db.Events.FirstOrDefault(e => e.Id == Id);
            if(obj == null)
            {
                return NotFound();
            }
            List<Comment> comments=_db.Comments.ToList();
            EventDetailsCommentViewModel eventDetailsCommentViewModel = new EventDetailsCommentViewModel()
            {
                Event=obj,
                Comment=new Comment(),
                Comments=comments

            };
            return View(eventDetailsCommentViewModel);
        }
        
        public IActionResult MyEvents()
        {

            List<Event> list = _db.Events.ToList();
            List<Event> myEvents= new List<Event>();
            foreach (Event obj in list)
            {
                if (obj.CreatedBy == User.Identity.Name)
                {
                    myEvents.Add(obj);
                }
            }
            return View(list);

        }
        [HttpPost]
        public IActionResult AddComment(EventDetailsCommentViewModel viewModel)
        {
            Comment comment = new Comment()
            {
               EventId=viewModel.Event.Id,
               CommentText=viewModel.Comment.CommentText,
               Author=User.Identity.Name,
               CreatedDate=DateTime.Now,

            };
            _db.Comments.Add(comment);
            _db.SaveChanges();
            TempData["Success"] = "Comment posted successfully";
            return RedirectToAction("EventDetails", "Event", new { Id = viewModel.Event.Id });
        }
        [Authorize(Roles="Admin")]
        public IActionResult ListAllEvents()
        {
            List<Event> events = _db.Events.ToList();
            return View(events);
        }



    }

}
