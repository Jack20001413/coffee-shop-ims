using CoffeeShopIMS.Models;
using CoffeeShopIMS.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShopIMS.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ISupplierRepository _supplierRepository;

        public SupplierController(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public IActionResult Index()
        {
            List<Supplier> suppliers = _supplierRepository.GetAll();
            return View(suppliers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Supplier supplier)
        {
            supplier.CreatedAt = DateTime.Now;
            supplier.UpdatedAt = DateTime.Now;
            _supplierRepository.Create(supplier);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var supplier = _supplierRepository.GetById(id);

            if (supplier is null) return NotFound();
            return View(supplier);
        }

        [HttpPut]
        public IActionResult Edit(Supplier supplier)
        {
            if (!ModelState.IsValid) return View(supplier);

            supplier.UpdatedAt = DateTime.Now;
            _supplierRepository.Update(supplier);

            return RedirectToAction(nameof(Index));
        }
    }
}
