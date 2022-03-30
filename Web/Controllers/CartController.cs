using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;
using Web.ViewModels;

namespace Web.Controllers
{
    public class CartController : Controller
    {

        private readonly ProductManager _productManager;

        public CartController(ProductManager productManager)
        {
            _productManager = productManager;
        }

        public async Task<IActionResult> Index()
        {
            var cookieData = Request.Cookies["myCookie"];

            if (cookieData != null && cookieData!="")
            {

                List<int> productIds = cookieData.Split("-").Select(x => int.Parse(x)).ToList();
                List<Product> productList = await _productManager.GetByIds(productIds.Distinct());
                CartVM vm = new()
                {
                    ProIds = productIds,
                    CartItems = productList,
                };
                return View(vm);
            }





            return View();
        }
    }
}
