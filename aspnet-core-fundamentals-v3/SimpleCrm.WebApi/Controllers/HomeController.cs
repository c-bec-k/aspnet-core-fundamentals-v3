using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimpleCrm.WebApi.Models;

namespace SimpleCrm.WebApi.Controllers
{
  [Route("")]
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
      _logger = logger;
    }

    [Route("")]
    [ResponseCache(Duration = 60 * 60 * 24 * 4, Location = ResponseCacheLocation.Any)]
    public IActionResult Index() => View();

    [Route("about")]
    public IActionResult About()
    {
      ViewData["Message"] = "Your application description page.";
      throw new Exception("Sample exception thrown. Please catch me!");
      // return View();
    }

    [Route("contact")]
    public IActionResult Contact()
    {
      ViewData["Message"] = "Your contact page.";

      return View();
    }

    [Route("privacy")]
    public IActionResult Privacy()
    {
      return View();
    }


    [Route("error")]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}
