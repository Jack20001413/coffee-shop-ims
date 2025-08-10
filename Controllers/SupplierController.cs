using coffee_shop_ims.Data;
using coffee_shop_ims.Models;
using Microsoft.AspNetCore.Mvc;

namespace coffee_shop_ims.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SupplierController(ApplicationDbContext repo)
        {
            _context = repo;
        }

        public IActionResult Index()
        {
            List<Supplier> suppliers = _context.Suppliers.ToList();
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
            _context.Suppliers.Add(supplier);

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
