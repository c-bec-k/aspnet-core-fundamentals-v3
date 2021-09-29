using System;
namespace SimpleCrm.WebApi.Models
{
  public class UserSummaryViewModel
  {
    public string Id { get; set; }
    public string Name { get; set; }
    public string emailAddress { get; set; }
    public string jwt { get; set; }
    public ArraySegment<String> Roles { get; set; }
    public string AccountID { get; set; }
  }
}
