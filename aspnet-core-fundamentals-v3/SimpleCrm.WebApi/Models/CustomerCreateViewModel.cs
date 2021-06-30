using System;
using System.ComponentModel.DataAnnotations;
using SimpleCrm;

namespace SimpleCrm.WebApi.Models {
  public class CustomerCreateViewModel
  {
        [MaxLength(50),Required()]
        public string FirstName { get; set; }

        [MinLength(1), MaxLength(50)]
        [Required()]
        public string LastName { get; set; }

        [MinLength(1), MaxLength(20)]
        public string PhoneNumber { get; set; }
    public string EmailAddress { get; set; }
    public InteractionMethod PreferredContactMethod { get; set; }
  }
}