using System;

namespace SimpleCrm
{
  public class CustomerListParameters
  {
    public int Page { get; set; }
    public int Take { get; set; }
    public string OrderBy { get; set; }
    public string LastName { get; set; }
    public string Term { get; set; }
  }
}
