using System;
using Microsoft.AspNetCore.Mvc;
using SimpleCrm.WebApi.Models;
using System.Linq;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace SimpleCrm.WebApi.ApiControllers
{

  [Route("api/customer")]
  [Authorize(Policy = "ApiUser")]
  public class CustomerController : Controller
  {
    private readonly ICustomerData _customerData;
    private readonly LinkGenerator _linkGenerator;
    private string GetCustomerResourceUri(CustomerListParameters listParameters, int pageAdjust)
    {
      if (listParameters.Page + pageAdjust <= 0)
      {
        return null;
      }
      return _linkGenerator.GetPathByName(this.HttpContext, "GetCustomers", values: new
      {
        take = listParameters.Take,
        page = listParameters.Page + pageAdjust,
        orderBy = listParameters.OrderBy
      });
    }

    public CustomerController(ICustomerData customerData, LinkGenerator linkGenerator)
    {
      _customerData = customerData;
      _linkGenerator = linkGenerator;
    }

    // [AllowAnonymous]
    [HttpGet("", Name = "GetCustomers")] // GET /api/customer
    public IActionResult GetAll([FromQuery] CustomerListParameters listParameters)
    {
      var customers = _customerData.GetAll(listParameters);
      var models = customers.Select(cust => new CustomerDisplayViewModel(cust));

      var pagination = new PaginationModel
      {
        next = customers.Count < listParameters.Take ? null : GetCustomerResourceUri(listParameters, 1),
        prev = listParameters.Page <= 1 ? null : GetCustomerResourceUri(listParameters, -1),
        //first = listParameters.Page == 1 ? null : GetCustomerResourceUri(listParameters, 0),
      };

      Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(pagination));

      return Ok(customers);
    }



    [HttpGet("{id}")] // GET /api/customer/:id
    [ResponseCache(Duration = 60 * 60 * 24 * 2, Location = ResponseCacheLocation.Client)]
    public IActionResult Get(int id)
    {
      if (id == 1)
      {
        throw new ApiException("Bad customer ID, please try again >_<");
      }
      var cust = _customerData.Get(id);
      if (cust == null)
      {
        return NotFound();
      }
      var model = new CustomerDisplayViewModel(cust);
      Response.Headers.Add("ETag", cust.LastUpdated.ToString());
      return Ok(model);
    }

    [HttpPost("")] // POST /api/customer
    public IActionResult Create([FromBody] CustomerCreateViewModel model)
    {
      if (model == null)
      {
        return BadRequest();
      }
      if (!ModelState.IsValid)
      {
        return UnprocessableEntity(ModelState);
      }

      var cust = new Customer
      {
        FirstName = model.FirstName,
        LastName = model.LastName,
        EmailAddress = model.EmailAddress,
        PhoneNumber = model.PhoneNumber,
        PeferredContactMethod = model.PreferredContactMethod,
        LastUpdated = DateTimeOffset.UtcNow
      };

      _customerData.Add(cust);
      _customerData.Commit();

      return Ok(new CustomerDisplayViewModel(cust));
    }

    [HttpPut("{id}")] // PUT /api/customer/:id
    public IActionResult Update(int id, [FromBody] CustomerDisplayViewModel model)
    {
      var cust = _customerData.Get(id);
      if (cust == null)
      {
        return NotFound();
      }

      if (!ModelState.IsValid)
      {
        return UnprocessableEntity(ModelState);
      }

      string ifMatch = Request.Headers["If-Match"];
      if (ifMatch != cust.LastUpdated.ToString())
      {
        return Conflict("ETag mis-match. Please try again.");
      }

        cust.FirstName = model.FirstName;
        cust.LastName = model.LastName;
        cust.PhoneNumber = model.PhoneNumber;
        cust.StatusCode = model.Status;
        cust.EmailAddress = model.EmailAddress;
        cust.LastUpdated = DateTimeOffset.UtcNow;

      _customerData.Update(cust);
      _customerData.Commit();
      return Ok(new CustomerDisplayViewModel(cust));
    }

    [HttpDelete("{id}")] // DELETE /api/customer/:id
    public IActionResult Delete(int id)
    {
      var cust = _customerData.Get(id);

      if (cust == null)
      {
        return NotFound();
      }

      _customerData.Delete(cust);
      _customerData.Commit();
      return NoContent();
    }

  }
}