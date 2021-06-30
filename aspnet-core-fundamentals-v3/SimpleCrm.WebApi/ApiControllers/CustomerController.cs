using System;
using Microsoft.AspNetCore.Mvc;
using SimpleCrm.WebApi.Models;
using System.Linq;

namespace SimpleCrm.WebApi.ApiControllers
{
 
  [Route("api/customer")]
  public class CustomerController : Controller
  {
    private readonly ICustomerData _customerData;

  public CustomerController(ICustomerData customerData) {
    _customerData = customerData;
  }

    [HttpGet("")] // GET /api/customer
    public IActionResult GetAll()
    {
      var customers = _customerData.GetAll(0, 50, "");
      var models = customers.Select( cust => new CustomerDisplayViewModel(cust));
      return Ok(customers);
    }

    [HttpGet("{id}")] // GET /api/customer/:id
    public IActionResult Get(int id)
    {
      var cust = _customerData.Get(id);
      if (cust == null) {
        return NotFound();
      }
      var model = new CustomerDisplayViewModel(cust);
      return Ok(model);
    }

    [HttpPost("")] // POST /api/customer
    public IActionResult Create([FromBody] CustomerCreateViewModel model)
    {
      if (model == null) {
        return BadRequest();
      }
      if (!ModelState.IsValid) {
        return UnprocessableEntity(ModelState);
      }

      var cust = new Customer {
        FirstName = model.FirstName,
        LastName = model.LastName,
        EmailAddress = model.EmailAddress,
        PhoneNumber = model.PhoneNumber,
        PeferredContactMethod = model.PreferredContactMethod
      };
      
      _customerData.Add(cust);
      _customerData.Commit();

      return Ok(new CustomerDisplayViewModel(cust));
    }

    [HttpPut("{id}")] // PUT /api/customer/:id
    public IActionResult Update(int id, [FromBody] CustomerDisplayViewModel model)
    {
      var cust = _customerData.Get(id);
      if (cust == null) {
        return NotFound();
      }

      cust.FirstName = model.FirstName;
      cust.LastName = model.LastName;
      cust.PhoneNumber = model.PhoneNumber;
      cust.StatusCode = model.Status;
      cust.EmailAddress = model.EmailAddress;
      // cust.LastContactDate = DateTimeOffset.UtcNow;

      _customerData.Update(cust);
      _customerData.Commit();
      return Ok(new CustomerDisplayViewModel(cust));
    }

    [HttpDelete("{id}")] // DELETE /api/customer/:id
    public IActionResult Delete(int id)
    {
      var cust = _customerData.Get(id);

      if (cust == null) {
        return NotFound();
      }

      _customerData.Delete(cust);
      _customerData.Commit();
      return NoContent();
    }

  }
}