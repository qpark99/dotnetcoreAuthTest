using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Login()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "email"),
                new Claim("LastChanged", "LastChanged")
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, 
                CookieAuthenticationDefaults.AuthenticationScheme);
            
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

            return Redirect("/Home/Info");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/Home/Info");
        }

        public IActionResult Info()
        {
            return Json(new
            {
                isAuthenticated = User.Identity.IsAuthenticated,
                name = User.Identity.Name,
                lastChanged = User.Claims.FirstOrDefault(claim => claim.Type == "LastChanged")?.Value
            });
        }
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}