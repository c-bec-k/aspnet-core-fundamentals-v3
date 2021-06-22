using System;
using Microsoft.AspNetCore.Mvc;

namespace SimpleCrm.WebApi.ApiControllers
{
  [Route("api/customer")]
  public class CustomerController : Controller
  {
    [HttpGet("")] // GET /api/customer
    public IActionResult GetAll()
    {
      throw new NotImplementedException();
    }

    [HttpGet("{id}")] // GET /api/customer/:id
    public IActionResult Get(int id)
    {
      throw new NotImplementedException();
    }

    [HttpPost("")] // POST /api/customer
    public IActionResult Create([FromBody] Customer model)
    {
      throw new NotImplementedException();
    }

    [HttpPut("{id}")] // PUT /api/customer/:id
    public IActionResult Update(int id, [FromBody] Customer model)
    {
      throw new NotImplementedException();
    }

    [HttpDelete("{id}")] // DELETE /api/customer/:id
    public IActionResult Delete(int id)
    {
      throw new NotImplementedException();
    }

  }
}