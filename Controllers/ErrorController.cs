using Microsoft.AspNetCore.Mvc;

namespace Enterprise_E_Commerce_Management_System.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index(int statusCode)
        {
            ViewData["Message"] = statusCode switch
            {
                400 => "Something went wrong with your request. Please try again.",//BadRequest
                404 => "We couldn't find what you were looking for.",//NotFound
                _ => "Oops! Something went wrong. Please try again later."//Exception
            };
            return View();
        }
    }
}
