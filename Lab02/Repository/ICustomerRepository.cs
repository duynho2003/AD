using Lab02.Models;

namespace Lab02.Repository
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetCustomers();
        Customer checkLogin(string email, string pass);
        bool PostCustomer(Customer newCustomer);
    }
}
