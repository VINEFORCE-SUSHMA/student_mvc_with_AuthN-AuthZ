using Microsoft.AspNetCore.Mvc;
using STUDENT.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;

namespace studentCRUD.Controllers
{
    public class AccountController : Controller
    {
        // Dummy users (replace with DB in production)
        private static List<User> users = new List<User>
        {
            new User { UserId=1, Email="admin@gmail.com", Password="123", Role="ADMIN" },
            new User { UserId=2, Email="user@gmail.com", Password="123", Role="USER" }
        };

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = users.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user != null)
            {
                HttpContext.Session.SetInt32("UserId", user.UserId);
                HttpContext.Session.SetString("Role", user.Role);
                return RedirectToAction("Index", "Students");
            }
            ModelState.AddModelError("", "Invalid email or password");
            return View();
        }

        // POST: Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            // Clear all session
            HttpContext.Session.Clear();

            // Redirect to login page
            return RedirectToAction("Login");
        }
    }
}
