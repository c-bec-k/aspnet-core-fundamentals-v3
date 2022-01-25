
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SimpleCrm.WebApi.Filters
{

  class JSONerror
  {
    public Boolean success { get; set; }
    public List<String> messages { get; set; }
    public Object item { get; set; }

  }
  public class GlobalExceptionFilter : IExceptionFilter, IDisposable
  {
    public void Dispose()
    {
      throw new NotImplementedException();
    }

    public void OnException(ExceptionContext context)
    {
      context.Result = new ObjectResult(new JSONerror
      {
        success = false,
        messages = new List<string> { context.Exception.Message, context.Exception.Source },
      })
      {
        StatusCode = 500
      };
    }
  }
}