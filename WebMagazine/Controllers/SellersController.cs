using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebMagazine.Data;
using WebMagazine.Models;
using WebMagazine.Models.ViewModels;

namespace WebMagazine.Controllers
{
    public class SellersController : Controller
    {
        private readonly WebMagazineContext _context;

        public SellersController(WebMagazineContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var sellers = _context.Seller.Include("Department").ToList();

            //Soma o salário de todos os vendedores
            ViewData["CustoRH"] = sellers.Sum(s => s.Salary);
            //Maior salário de todos os vendedores
            ViewData["Maior"] = sellers.Max(s => s.Salary);
            //Menor salário de todos os vendedores
            ViewData["Menor"] = sellers.Min(s => s.Salary);
            //Média salarial de todos os vendedores
            ViewData["Media"] = sellers.Average(s => s.Salary);
            //Conta quantos vendedores ganham bem
            ViewData["Ricos"] = sellers.Count(s => s.Salary >= 30000);
            //Filtra os vendedores com salário abaixo de 20k
            var poorSellers = sellers.Where(s => s.Salary < 20000).ToArray();

            var sellerAscNameSalary = sellers
                //.Where(s => s.Salary > 1000)
                .OrderBy(s => s.Name)
                .ThenBy(s => s.Salary);

            return View(sellerAscNameSalary);

        }

        public IActionResult Create()
        {
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
            var seller = _context.Seller
                .Include("Department")
                .FirstOrDefault(s => s.Id == id);

            if (seller == null)
            {
                return NotFound();
            }

            seller.Sales = _context.SalesRecord
                .Where(sr => sr.SellerId == id).ToArray();

            //ano, mês, dia
            DateTime initial = new DateTime(2023, 09, 01);
            DateTime final = DateTime.Now;
            ViewData["total"] = seller.TotalSales(initial, final);
            //ViewData["total"] = seller.TotalSales(
            //        new DateTime(2023, 09, 01), DateTime.Now);
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
        public IActionResult Edit(Seller seller)
        {
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

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //Busca o vendedor no banco de dados
            var seller = _context.Seller.Include("Department").FirstOrDefault(s => s.Id == id);
            //Verifica se o vendedor existe
            if (seller == null)
            {
                return NotFound();
            }
            return View(seller);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var seller = _context.Seller.FirstOrDefault(s => s.Id == id);
            if (seller == null)
            {
                return NotFound();
            }
            _context.Remove(seller);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
