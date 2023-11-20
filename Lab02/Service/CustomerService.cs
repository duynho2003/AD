using Lab02.Models;
using Lab02.Repository;
using Microsoft.EntityFrameworkCore;

namespace Lab02.Service
{
    public class CustomerService : ICustomerRepository
    {
        private readonly AddbContext _dbContext;
        public CustomerService(AddbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Customer checkLogin(string email, string pass)
        {
            var customer = _dbContext.Customers.SingleOrDefault(
                    c=>c.Email == email);
            if(customer == null)
            {
                return null!;
            }
            else
            {
                if(customer.Password == pass) 
                {
                    return customer;
                }
                else
                {
                    return null!;
                }
            }
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return _dbContext.Customers.ToList();
        }

        public bool PostCustomer(Customer newCustomer)
        {
             _dbContext.Customers.Add(newCustomer);
             _dbContext.SaveChanges();
            return true;
        }
    }
}
