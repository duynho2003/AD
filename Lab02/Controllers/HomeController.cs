using Lab02.Models;
using Lab02.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Lab02.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository productRepository;

        public HomeController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = productRepository.GetProducts();
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product, IFormFile file)
        {
            try
            {
                if (file == null)
                {
                    ViewBag.msg = "File not selected";
                }
                else
                {
                    var path = Path.Combine("wwwroot/images", file.FileName);
                    var stream = new FileStream(path, FileMode.Create);
                    file.CopyToAsync(stream);
                    product.Photo = "images/" + file.FileName;

                    productRepository.PostProduct(product);

                    return RedirectToAction("Index", "Home");
                }
            }   
            catch (Exception ex) 
            { 
                ViewBag.msg = ex.Message;
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}