using Lab02.Models;
using Lab02.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Lab02.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }
        public IActionResult Index()
        {
            var model = customerRepository.GetCustomers();
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]        
        public IActionResult Create(Customer customer)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    customerRepository.PostCustomer(customer);
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Fail");
                }
            }
            catch (Exception ex) 
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]        
        public IActionResult Login(string email, string pass)
        {
            try
            {
                if (customerRepository.checkLogin(email, pass)!=null)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Fail");
                }
            }
            catch (Exception ex) 
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View();
        }
    }
}
