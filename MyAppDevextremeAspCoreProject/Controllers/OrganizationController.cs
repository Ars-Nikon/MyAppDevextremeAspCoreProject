using DevExpress.Xpo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using MyAppDevextremeAspCoreProject.Contexts;
using System;

namespace MyAppDevextremeAspCoreProject.Controllers
{
    public class OrganizationController : Controller
    {
        readonly ApplicationContext _appContext;
        public OrganizationController(ApplicationContext applicationContext)
        {
            _appContext = applicationContext;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}