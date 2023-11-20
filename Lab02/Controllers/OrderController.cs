using Lab02.Helper;
using Lab02.Models;
using Lab02.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Lab02.Controllers
{
    public class OrderController : Controller
    {
        private readonly IProductRepository productRepository;

        public OrderController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public IActionResult Index()
        {
            var cart = SessionHelper.GetObjectJson<List<OrderDetail>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            ViewBag.subtotal = cart!.Sum(item => item.Product!.Price * item.Quantity);
            return View();
        }
        public int checkProduct(int id) 
        { 
            List<OrderDetail> cart= SessionHelper.GetObjectJson<List<OrderDetail>>(HttpContext.Session, "cart");
            for(int i = 0;i<cart!.Count;i++) 
            {
                if (cart[i].Product!.ProductId.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }
        public IActionResult Remove(int id) 
        { 
            List<OrderDetail> cart= SessionHelper.GetObjectJson<List<OrderDetail>>(HttpContext.Session, "cart");
            int index = checkProduct(id);
            cart!.RemoveAt(index);
            SessionHelper.SetObjectJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index", "Order");
        }
        public IActionResult Buy(int id)
        {
            var model = productRepository.GetProducts();
            List<OrderDetail> cart;
            if(SessionHelper.GetObjectJson<List<OrderDetail>>(HttpContext.Session, "cart") == null)
            {
                cart = new List<OrderDetail>();
                cart.Add(new OrderDetail 
                {
                    Product=model.Single(p=>p.ProductId == id),
                    Quantity=1
                });
                SessionHelper.SetObjectJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                cart= SessionHelper.GetObjectJson<List<OrderDetail>>(HttpContext.Session, "cart");
                int index = checkProduct(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                    SessionHelper.SetObjectJson(HttpContext.Session, "cart", cart);
                }
                else
                {
                    cart.Add(new OrderDetail
                    {
                        Product = model.Single(p => p.ProductId == id),
                        Quantity = 1
                    });
                    SessionHelper.SetObjectJson(HttpContext.Session, "cart", cart);
                }
            }
            return RedirectToAction("Index", "Order");
        }

        public IActionResult Success() 
        { 
            ViewBag.msg = "Order successfully";
            SessionHelper.SetObjectJson(HttpContext.Session, "cart", null!);
            return View();
        }
    }
}
