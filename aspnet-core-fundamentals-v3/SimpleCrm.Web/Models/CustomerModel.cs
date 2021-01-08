using System;
namespace SimpleCrm.Web.Models
{
    public class CustomerModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }

        public CustomerModel(int Id, string FirstName, string LastName, string PhoneNumber)
        {
            this.Id = Id;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.PhoneNumber = PhoneNumber;
        }
    }
    
}
