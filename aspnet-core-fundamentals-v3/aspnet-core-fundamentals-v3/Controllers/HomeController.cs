using aspnet_core_fundamentals_v3.Models;
using Microsoft.AspNetCore.Mvc;

namespace aspnet_core_fundamentals_v3.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var model = new CustomerModel(1, "Christopher", "Ruthenbeck", "512.555.5555");
            return View(model);
        }
    }
}
