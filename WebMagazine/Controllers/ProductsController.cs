using Microsoft.AspNetCore.Mvc;
using WebMagazine.Models;

namespace WebMagazine.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            /* Instancia uma lista de produtos */
            List<Product> products = new List<Product>();

            Product p = new Product();
            p.Id = 1;
            p.Name = "TV LG 29'";
            products.Add(p);

            products.Add(new Product { Id = 2, Name = "Geladeira LG" });
            products.Add(new Product { Id = 3, Name = "Torradeira LG" });

            return View(products);
        }
    }
}
