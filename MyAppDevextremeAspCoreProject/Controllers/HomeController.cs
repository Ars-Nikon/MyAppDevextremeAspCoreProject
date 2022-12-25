using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Bogus;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyAppDevextremeAspCoreProject.Contexts;
using MyAppDevextremeAspCoreProject.Models;
using MyAppDevextremeAspCoreProject.Utilities;

namespace MyAppDevextremeAspCoreProject.Controllers
{
    public class HomeController : Controller
    {
        readonly ApplicationContext _appContext;
        readonly ILogger<HomeController> _logger;
        public HomeController(ApplicationContext applicationContext, ILogger<HomeController> logger)
        {
            _logger = logger;
            _appContext = applicationContext;
        }
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string? ReturnUrl, UserLogin userLogin)
        {
            if (!string.IsNullOrEmpty(ReturnUrl))
            {
                ViewBag.ReturnUrl = ReturnUrl;
            }
            if (!ModelState.IsValid)
            {
                return View(userLogin);
            }
            try
            {
                var passwordHash = Utilities.Utils.GetSHA256(userLogin.Password);
                var user = await _appContext.Users.FirstOrDefaultAsync(x => x.Login == userLogin.Login);
                if (user == null || !(user.PasswordHash.SequenceEqual(passwordHash)))
                {
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
                    return View(userLogin);
                }
                var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Login) };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return Redirect(ReturnUrl ?? "/");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return View(userLogin);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Home");
        }
    }
}
