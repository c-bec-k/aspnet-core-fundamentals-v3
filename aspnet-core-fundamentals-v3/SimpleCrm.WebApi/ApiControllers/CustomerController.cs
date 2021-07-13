using System;
using Microsoft.AspNetCore.Mvc;
using SimpleCrm.WebApi.Models;
using System.Linq;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;

namespace SimpleCrm.WebApi.ApiControllers
{
 
  [Route("api/customer")]
  public class CustomerController : Controller
  {
    private readonly ICustomerData _customerData;
    private readonly LinkGenerator _linkGenerator;
    private string GetCustomerResourceUri(int page, int take) {
      return _linkGenerator.GetPathByName(this.HttpContext, "GetCustomers", values: new {
        page, take
      });
    }

  public CustomerController(ICustomerData customerData, LinkGenerator linkGenerator) {
    _customerData = customerData;
    _linkGenerator = linkGenerator;
  }

    [HttpGet("", Name = "GetCustomers")] // GET /api/customer
    public IActionResult GetAll([FromQuery]int page = 1, [FromQuery]int take = 50)
    {
      var customers = _customerData.GetAll(page - 1, take, "");
      var models = customers.Select( cust => new CustomerDisplayViewModel(cust));

      var pagination = new PaginationModel{
        next = customers.Count < take ? null : GetCustomerResourceUri(page+1, take),
        prev = page <= 1? null : GetCustomerResourceUri(page-1, take),
        first = page == 1 ? null : GetCustomerResourceUri(1, take),
        // last = customers.Count < take ? null : GetCustomerResourceUri((customers.Count / take) + (customers.Count % take > 1 ? 1 : 0), take) 
      };

      Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(pagination));

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

      if (!ModelState.IsValid)
      {
        return UnprocessableEntity(ModelState);
      }

      var newCust = new Customer
      {
        FirstName = model.FirstName,
        LastName = model.LastName,
        PhoneNumber = model.PhoneNumber,
        StatusCode = model.Status,
        EmailAddress = model.EmailAddress
      };

      _customerData.Update(newCust);
      _customerData.Commit();
      return Ok(new CustomerDisplayViewModel(newCust));
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