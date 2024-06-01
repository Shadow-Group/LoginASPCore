using Login.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Login.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserDbContext userDb;

        public HomeController(UserDbContext userDb)
        {
            this.userDb = userDb;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                return RedirectToAction("Dashboard");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            var myUser =await userDb.Users.Where(x => x.Email == user.Email && x.Password == user.Password).FirstOrDefaultAsync();

            if(myUser != null)
            {
                HttpContext.Session.SetString("UserSession", myUser.Email);
                return RedirectToAction("Dashboard");
            }
            else
            {
                ViewBag.Message = "Login Failed";
            }
            return View();
        }

        public async Task<IActionResult> Logout(User user)
        {

            if (HttpContext.Session.GetString("UserSession") != null)
            {
                HttpContext.Session.Remove("UserSession");
                return RedirectToAction("Login");
            }
            return View();
        }

        public IActionResult Dashboard()
        {
            if(HttpContext.Session.GetString("UserSession") !=null)
            {
                ViewBag.MySession = HttpContext.Session.GetString("UserSession").ToString();
            }
            else
            {
                return RedirectToAction("Login");
            }

            return View();
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            if(ModelState.IsValid)
            {
                await userDb.Users.AddAsync(user);
                await userDb.SaveChangesAsync();
                TempData["Success"] = "Registered Successfully";
                return RedirectToAction("Login");
            }
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
