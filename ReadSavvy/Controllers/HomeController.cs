using ReadSavvy.Data;
using ReadSavvy.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;
using System.ComponentModel;
using ReadSavvy.Services;

namespace ReadSavvy.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        //private LogService _log;

        
        
            
        
        public HomeController(ApplicationDbContext db, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;

        }
        
        public IActionResult Index()
        {
            IEnumerable<Event> eventList = _db.Events.ToList();

            return View(eventList);
        }
        
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        
        public async Task<IActionResult> Login(User obj)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(obj.Email, obj.Password, false, false);
                if (result.Succeeded)
                {
                    LogService.getInstance.LogMessage("Login Successfull");
                    return RedirectToAction("Index", "Event");
                }
                ModelState.AddModelError("", "Invalid Credentials");
            }
            return View(obj);

        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(User obj)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser() { UserName = obj.Email, Email = obj.Email, Name = obj.FullName };
                var result = await _userManager.CreateAsync(user, obj.Password); //bool result, identity result
                //if user created successfully
                if (result.Succeeded)
                {
                    //await _signInManager.CreateAsync(user, isPersistent: false);//session key using
                    TempData["Success"] = "Registration Successfull";
                    return RedirectToAction("Login");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }


            }
            return View(obj);

        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            TempData["Success"] = "Sucessfully Logout";
            return RedirectToAction("Login");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}