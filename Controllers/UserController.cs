using System.Security.Claims;
using FINALPROJECT.Domain.Models.RequestModel;
using FINALPROJECT.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace FINALPROJECT.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserRequestModel request)
        {
            var response = await _userService.Login(request);
            if (response.Status)
            {
                CookieOptions cookie = new CookieOptions();
                cookie.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Append("Id", response.Data.Id, cookie);
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, response.Data.Id.ToString()),
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(claimsIdentity);
                var property = new AuthenticationProperties();
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, property);
                return RedirectToAction("Index", "Auction");

            }
            ViewBag.ErrorMessage = response.Message;
            return View(request);
        }

      

    }
}
