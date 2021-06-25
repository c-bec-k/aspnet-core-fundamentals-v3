using System;
using System.ComponentModel.DataAnnotations;

namespace SimpleCrm
{
    public class Customer
    {
        public int Id { get; set; }

        [MaxLength(50)]
        [Required()]
        public string FirstName { get; set; }

        [MinLength(1), MaxLength(50)]
        [Required()]
        public string LastName { get; set; }

        [MinLength(1), MaxLength(20)]
        public string PhoneNumber { get; set; }

        public bool OptInNewsletter { get; set; }
        public CustomerType Type { get; set; }

        [Required()]
        public string EmailAddress { get; set; }

        public InteractionMethod PeferredContactMethod { get; set; }
        public CustomerStatus StatusCode { get; set; }
        public DateTimeOffset LastContactDate { get; set; }
    }

    public enum InteractionMethod
    {
        None = 0,
        Email = 1,
        Phone = 2
    }

    public enum CustomerStatus
    {
        not_interested = 0,
        customer = 1,
        prospect = 2,
        unknown = 3
    }
}