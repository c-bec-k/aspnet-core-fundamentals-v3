using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace SimpleCrm.SqlDbServices
{
  public class SqlCustomerData : ICustomerData
  {
    private SimpleCrmDbContext _context;

    public SqlCustomerData(SimpleCrmDbContext context)
    {
      _context = context;
    }

    public Customer Get(int id)
    {
      return _context.Customers.FirstOrDefault(x => x.Id == id);
    }

    public IEnumerable<Customer> GetAll()
    {
      return _context.Customers.ToList();
    }

    public void Add(Customer customer)
    {
      _context.Customers.Add(customer);
    }

    public void Update(Customer customer)
    {

    }

    public void Commit()
    {
      _context.SaveChanges();
    }

    public List<Customer> GetAll(CustomerListParameters listParameters)
    {
      var allowedFields = new string[] { "firstname", "lastname", "emailaddress", "customerstatus" };
      if (string.IsNullOrWhiteSpace(listParameters.OrderBy))
      {
        listParameters.OrderBy = "lastname asc";
      }
      var orderBy = listParameters.OrderBy;
      var sorts = (orderBy ?? "").Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);
      foreach (var sort in sorts)
      {
        var field = sort.Trim().ToLower();
        var parts = field.Split(" ");

        if (parts.Length > 2)
        {
          throw new System.Exception("invalid number of args");
        }
        if (parts.Length > 1 && parts[1] != "asc" && parts[1] != "desc")
        {
          throw new System.Exception("Invalid sort function");
        }

        if (!allowedFields.Contains(parts[0]))
        {
          throw new System.Exception("Invalid sort fields");
        }
      }

      
      IQueryable<Customer> sortedResults = _context.Customers.OrderBy(orderBy);

      if (!string.IsNullOrWhiteSpace(listParameters.LastName))
      {
        sortedResults = sortedResults.Where(cust => cust.LastName.ToLowerInvariant() == listParameters.LastName.Trim().ToLowerInvariant());
      }

      if (!string.IsNullOrWhiteSpace(listParameters.FirstName))
      {
        sortedResults = sortedResults.Where(cust => cust.FirstName.ToLowerInvariant() == listParameters.FirstName.Trim().ToLowerInvariant());
      }

      if (!string.IsNullOrWhiteSpace(listParameters.Term))
      {
        sortedResults = sortedResults.Where(cust => (cust.FirstName + " " + cust.LastName).Contains(listParameters.Term)
                        || cust.EmailAddress.Contains(listParameters.Term));
      }



      return sortedResults.Skip((listParameters.Page - 1) * listParameters.Take)
      .Take(listParameters.Take).ToList();
    }

    public void Delete(Customer item)
    {
      _context.Remove(item);
    }
  }
}