using System;

namespace SimpleCrm.WebApi.Models
{
  public class CustomerDisplayViewModel
  {
    public int CustomerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string EmailAddress { get; set; }
    public InteractionMethod PreferredContactMethod { get; set; }
    public CustomerStatus Status { get; set; }
    public DateTimeOffset LastUpdated { get; set; }

    public CustomerDisplayViewModel() { }

    public CustomerDisplayViewModel(Customer src)
    {
      if (src == null) return;
      CustomerId = src.Id;
      FirstName = src.FirstName;
      LastName = src.LastName;
      PhoneNumber = src.PhoneNumber;
      EmailAddress = src.EmailAddress;
      PreferredContactMethod = src.PeferredContactMethod;
      Status = src.StatusCode;
      LastUpdated = src.LastUpdated;
    }
  }
}