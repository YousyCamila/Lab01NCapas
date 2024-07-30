using Microsoft.AspNetCore.Mvc;

namespace WebApplicationOrders.Controllers
{
    public class CustomersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
