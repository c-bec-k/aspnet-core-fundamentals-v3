using System.Collections.Generic;
using SimpleCrm;

namespace aspnet_core_fundamentals_v3.Models.Home
{
    public class HomePageViewModel
    {
        public string CurrentMessage { get; set; }
        public IEnumerable<Customer> Customers { get; set; }
    }
}
