using System;
using aspnet_core_fundamentals_v3.Models.Home;
using Microsoft.AspNetCore.Mvc;
using SimpleCrm;

namespace aspnet_core_fundamentals_v3.Controllers
{
    public class HomeController : Controller
    {
        private ICustomerData _customerData;
        private readonly IGreeter _greeter;

        public HomeController(ICustomerData customerData, IGreeter greeter)
        {
            _customerData = customerData;
            _greeter = greeter;
        }

        public IActionResult Details(int id)
        {
            var customer =  _customerData.Get(id);
            if (customer == null) { return RedirectToAction(nameof(Index)); }
            return View(customer);
        }

        public IActionResult Index()
        {
            var model = new HomePageViewModel();
            model.Customers = _customerData.GetAll();
            model.CurrentMessage = _greeter.GetGreeting();
            return View(model);
        }

        [HttpGet()]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost()]
        public IActionResult Create(CustomerEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var customer = new Customer
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    OptInNewsletter = model.OptInNewsletter,
                    Type = model.Type
                };
                _customerData.Save(customer);
                return RedirectToAction(nameof(Details), new { id = customer.Id });
            }

            return View();

        }
    }
}
