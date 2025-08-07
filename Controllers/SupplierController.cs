using coffee_shop_ims.Data;
using coffee_shop_ims.Models;
using Microsoft.AspNetCore.Mvc;

namespace coffee_shop_ims.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ApplicationDbContext _repo;

        public SupplierController(ApplicationDbContext repo)
        {
            _repo = repo;
        }

        public ActionResult Index()
        {
            List<Supplier> suppliers = _repo.Suppliers.ToList();
            return View(suppliers);
        }

    }
}
