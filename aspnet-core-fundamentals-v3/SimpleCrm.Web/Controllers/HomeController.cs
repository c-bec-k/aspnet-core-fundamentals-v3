using System;
using SimpleCrm.Web.Models.Home;
using Microsoft.AspNetCore.Mvc;

namespace SimpleCrm.Web.Controllers
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

        [HttpGet()]
        public IActionResult Edit(int id)
        {
            var customer = _customerData.Get(id);
            if (customer == null) { return RedirectToAction(nameof(Index)); }

            var model = new CustomerEditViewModel
            {
              Id = customer.Id,
              FirstName = customer.FirstName,
              LastName = customer.LastName,
              PhoneNumber = customer.PhoneNumber,
              OptInNewsletter = customer.OptInNewsletter,
              Type = customer.Type
            };
            return View(model);
        }

        [HttpPost()]
        [ValidateAntiForgeryToken()]
        public IActionResult Edit(CustomerEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var customer = _customerData.Get(model.Id);
                if (customer == null) { return RedirectToAction(nameof(Index)); }

                customer.FirstName = model.FirstName;
                customer.LastName = model.LastName;
                customer.PhoneNumber = model.PhoneNumber;
                customer.OptInNewsletter = model.OptInNewsletter;
                customer.Type = model.Type;

                _customerData.Update(customer);
                return RedirectToAction(nameof(Details), new { id = customer.Id });
            }
            return View();
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
        [ValidateAntiForgeryToken()]
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
                _customerData.Add(customer);
                return RedirectToAction(nameof(Details), new { id = customer.Id });
            }

            return View();

        }
    }
}
