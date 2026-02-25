using Enterprise_E_Commerce_Management_System.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client.Extensions.Msal;
using System.Diagnostics;
namespace Enterprise_E_Commerce_Management_System.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }  
        public IActionResult Privacy()
        {
            return View();
        } 
    }
}
