using Microsoft.AspNetCore.Mvc;
using SimpleCrm;

namespace aspnet_core_fundamentals_v3.Controllers
{
    public class HomeController : Controller
    {
        private ICustomerData _customerData;

        public HomeController(ICustomerData customerData)
        {
            _customerData = customerData;
        }

        public IActionResult Index()
        {
            var model = _customerData.GetAll();
            return View(model);
        }
    }
}
