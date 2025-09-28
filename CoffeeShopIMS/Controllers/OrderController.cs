using CoffeeShopIMS.Models;
using CoffeeShopIMS.Repositories;
using CoffeeShopIMS.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShopIMS.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public IActionResult Index()
        {
            var orders = _orderRepository.GetAll();
            return View(orders);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(PurchaseRequestViewModel data)
        { 
            // TODO: Not implemented yet
            return RedirectToAction(nameof(Index));
        }

    }
}
