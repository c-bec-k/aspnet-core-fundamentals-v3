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

      public List<Customer> GetAll(int pageIndex, int take, string orderBy)
      {
        var allowedFields = new string[] { "firstname", "lastname", "emailaddress", "customerstatus" };

        var sorts = orderBy.Split(new[] {','}, System.StringSplitOptions.RemoveEmptyEntries);
        foreach (var sort in sorts) {
          var field = sort.Trim().ToLower();
          var parts = field.Split(" ");

          if (parts.Length > 2) {
            throw new System.Exception("invalid number of args");
          }
          if ( parts.Length > 1 && parts[1] != "ASC" && parts[1] != "DESC") {
            throw new System.Exception("Invalid sort function");
          }

          if (!allowedFields.Contains(field)) {
            throw new System.Exception("Invalid sort fields");
          }
        }
        if (string.IsNullOrWhiteSpace(orderBy)) {
          orderBy = "lastname asc";
        }
        var items = _context.Customers
        .OrderBy(orderBy)
        .Skip(pageIndex * take)
        .Take(take);

        return items.ToList();
      }

      public void Delete(Customer item)
      {
        _context.Remove(item);
      }
  }
}
