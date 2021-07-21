using System.Collections.Generic;

namespace SimpleCrm
{
  public interface ICustomerData
  {
    Customer Get(int id);
    void Add(Customer customer);
    List<Customer> GetAll(CustomerListParameters listParameters);
    void Update(Customer customer);
    void Delete(Customer item);
    void Commit();
    IEnumerable<Customer> GetAll();
  }

}
