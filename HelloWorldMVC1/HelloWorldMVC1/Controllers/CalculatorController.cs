using Microsoft.AspNetCore.Mvc;
using HelloWorldMVC1.Models;

namespace HelloWorldMVC1.Controllers
{
    public class CalculatorController : Controller
    {

        /*
         Get
        */
        
        public IActionResult Index()
        {
            MCalculator calc = new MCalculator();
            

            return View(calc);
        }
        
        [HttpPost]
        public IActionResult Index(MCalculator calc, string action)
        {
            if(action=="add")
            {
                calc.Add();
            }

            if(action=="min")
            {
                calc.Min();
            }

            if(action=="div")
            {
                calc.Div();
            }

            if (action == "multi")
            {
                calc.Multi();
            }

            return View(calc);
        }

    }
}
