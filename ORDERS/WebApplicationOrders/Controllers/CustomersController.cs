using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using ProxyServer;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;

namespace WebApplicationOrders.Controllers
{
    public class CustomerController : Controller
    {
        private readonly CustomerProxy _proxy;

        public CustomerController()
        {
            this._proxy = new CustomerProxy();

        }
        public async Task<IActionResult> Index()

        {
            var customers = await _proxy.GetAllAsync();
            return View(customers);
        }

        public ActionResult Create()
        {
            return View();
        }
        //POST: Customer/Ceate
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,City,Country,Phone")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _proxy.CreateAsync(customer);
                    if (result == null)
                    {
                        return RedirectToAction("Error", new { message = "El cliente con el mismo nombre y apellido ya existe " });
                    }
                    return RedirectToAction(nameof(Index));
                }

                catch (Exception ex)
                {
                    return RedirectToAction("Error", new { message = ex.Message });
                }
            }
            return View(customer);
        }
    }
}

