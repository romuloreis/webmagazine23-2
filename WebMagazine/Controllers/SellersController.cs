using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging.Signing;
using WebMagazine.Data;
using WebMagazine.Models;
using WebMagazine.Models.ViewModels;

namespace WebMagazine.Controllers
{
    public class SellersController : Controller
    {
        private readonly WebMagazineContext _context;

        public SellersController(WebMagazineContext context) { 
            _context = context;
        }

        public IActionResult Index()
        {
            var sellers = _context.Seller.ToList();
            return View(sellers);
        }

        public IActionResult Create() {
            //Cria uma instância do SellerFormViewModel,
            //que vai ter duas
            //propriedades, a primeira é a lista de departamentos
            var viewModel = new SellerFormViewModel();
            //Carrega os departamentos do banco
            viewModel.Departments = _context.Department.ToList();
            //Encaminha os dados para a view
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(Seller seller)
        {
            /*Teste se foi passado um objeto vendedor*/
            if (seller == null)
            {   
                /*Retorna não encontrado se não for 
                 passado um objeto vendedor*/
                return NotFound();
            }

            //seller.Department = _context.Department.FirstOrDefault();
            //seller.DepartmentId = _context.Department.FirstOrDefault().Id;

            /*Adiciona o vendedor no banco*/
            _context.Add(seller);
            /*Confirmar adição do vendedor no banco*/
            _context.SaveChanges();
            /*Redireciona para a action index*/
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            //Verificar se existe um vendedor com
            //    esse ID no banco de dados
            var seller = _context.Seller.FirstOrDefault(s => s.Id == id);

            if (seller == null) { 
                return NotFound();
            }     
            
            return View(seller);
        }

        public IActionResult Edit(int id)
        {
            //Verificar se existe um vendedor com
            //    esse ID no banco de dados
            var seller = _context.Seller.FirstOrDefault(s => s.Id == id);
            //Verifica se foi encontrado um objeto
            //vendedor com o id passado na url
            if (seller == null)
            {
                return NotFound();
            }

            //Cria uma lista de departamentos
            List<Department> departments = _context.Department.ToList();

            //Cria uma instância do viewmodel
            SellerFormViewModel viewModel = new SellerFormViewModel();
            viewModel.Departments = departments;
            viewModel.Seller = seller;

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(Seller seller) { 
            //VerificationAllowListEntry se 
            //    foi passado um objeto 
            if (seller == null)
            {
                return NotFound();
            }
            _context.Update(seller);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
