using System.Collections.Generic;

namespace SimpleCrm
{
    public interface ICustomerData
    {
        IEnumerable<Customer> GetAll();
        Customer Get(int id);
        void Add(Customer customer);
        List<Customer> GetAll(int pageIndex, int take, string orderBy);
        void Update(Customer customer);
        void Delete(Customer item);
        void Commit();
    }

}
