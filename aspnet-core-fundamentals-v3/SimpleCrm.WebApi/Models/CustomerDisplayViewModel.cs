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
      public string PreferredContactMethod { get; set; }
      public string Status { get; set; }
      public string LastContactDate { get; set; }

      public CustomerDisplayViewModel() { }

      public CustomerDisplayViewModel(Customer src)
      {
        if (src == null) return;
        CustomerId = src.Id;
        FirstName = src.FirstName;
        LastName = src.LastName;
        PhoneNumber = src.PhoneNumber;
        EmailAddress = src.EmailAddress;
        PreferredContactMethod = Enum.GetName(typeof(InteractionMethod), src.PeferredContactMethod);
        Status = Enum.GetName(typeof(CustomerStatus), src.StatusCode);
      }
  }
}