using HelloWorldMVC1.Models;
using Microsoft.AspNetCore.Mvc;

namespace HelloWorldMVC1.Controllers
{
    public class HelloWorld : Controller
    {
        public IActionResult Index()
        {
            Students std = new Students(1, "sourav das", "Mvc");
            return View(std);
        }
    }
}
