using System;
using System.ComponentModel.DataAnnotations;

namespace SimpleCrm.Web.Models.Home
{
    public class CustomerEditViewModel
    {
        public int Id { get; set; }

        [Display(Name = "First Name")]
        [Required()]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required()]
        [MaxLength(20)]
        public string LastName { get; set; }

        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [Required()]
        public string PhoneNumber { get; set; }

        [Display(Name = "Opt in to the newsletter?")]
        public bool OptInNewsletter { get; set; }

        [Display(Name = "Customer Type")]
        public CustomerType Type { get; set; }
    }
}
