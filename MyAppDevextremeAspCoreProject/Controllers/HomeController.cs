using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyAppDevextremeAspCoreProject.Contexts;

namespace MyAppDevextremeAspCoreProject.Controllers
{
    public class HomeController : Controller
    {
        readonly ApplicationContext _appContext;
        public HomeController(ApplicationContext applicationContext)
        {
            _appContext = applicationContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
