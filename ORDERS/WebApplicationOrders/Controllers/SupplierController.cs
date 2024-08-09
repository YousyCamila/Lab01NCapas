﻿using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using ProxyServer;

namespace WebApplicationOrders.Controllers
{
    public class SupplierController : Controller
    {
        private readonly SupplierProxy _proxy;

        public SupplierController()
        {
            this._proxy = new SupplierProxy();
        }

        public async Task<IActionResult> Index()
        {
            var suppliers = await _proxy.GetAllAsync();
            return View(suppliers);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CompanyName,ContactName,ContactTitle,City,Country,Phone,Fax")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _proxy.CreateAsync(supplier);
                    if (result == null)
                    {
                        return RedirectToAction("Error", new { message = "El proveedor con el mismo nombre de empresa ya existe." });
                    }
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Error", new { message = ex.Message });
                }
            }
            return View(supplier);
        }

        public async Task<IActionResult> Edit(int Id)
        {
            var supplier = await _proxy.GetByIdAsync(Id);
            if (supplier == null)
            {
                return NotFound();
            }
            return View(supplier);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CompanyName,ContactName,ContactTitle,City,Country,Phone,Fax")] Supplier supplier)
        {
            if (id != supplier.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _proxy.UpdateAsync(id, supplier);
                    if (!result)
                    {
                        return RedirectToAction("Error", new { message = "No se puede realizar la edición porque hay duplicidad de nombre con otro proveedor." });
                    }
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Error", new { message = ex.Message });
                }
            }
            return View(supplier);
        }

        public async Task<IActionResult> Details(int Id)
        {
            var supplier = await _proxy.GetByIdAsync(Id);
            if (supplier == null)
            {
                return NotFound();
            }
            return View(supplier);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var supplier = await _proxy.GetByIdAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }
            return View(supplier);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var result = await _proxy.DeleteAsync(id);
                if (!result)
                {
                    return RedirectToAction("Error", new { message = "No se puede eliminar el proveedor porque tiene productos asociados." });
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", new { message = ex.Message });
            }
        }

        public IActionResult Error(string message)
        {
            ViewBag.ErrorMessage = message;
            return View();
        }
    }
}
