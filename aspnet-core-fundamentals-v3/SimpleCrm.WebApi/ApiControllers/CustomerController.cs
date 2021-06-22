using System;
using Microsoft.AspNetCore.Mvc;

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
      return Ok(customers);
    }

    [HttpGet("{id}")] // GET /api/customer/:id
    public IActionResult Get(int id)
    {
      var cust = _customerData.Get(id);
      if (cust == null) {
        return NotFound();
      }
      return Ok(cust);
    }

    [HttpPost("")] // POST /api/customer
    public IActionResult Create([FromBody] Customer model)
    {
      var cust = model;
      _customerData.Add(cust);
      _customerData.Commit();

      return Ok();
    }

    [HttpPut("{id}")] // PUT /api/customer/:id
    public IActionResult Update(int id, [FromBody] Customer model)
    {
      var cust = _customerData.Get(id);
      if (cust ==null) {
        return NotFound();
      }
      _customerData.Update(cust);
      _customerData.Commit();
      return Ok();
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