﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebMagazine.Data;
using WebMagazine.Models;
using WebMagazine.Models.ViewModels;

namespace WebMagazine.Controllers
{
    public class SalesRecordsController : Controller
    {
        private readonly WebMagazineContext _context;

        public SalesRecordsController(WebMagazineContext context)
        {
            _context = context;
        }

        // GET: SalesRecords
        public async Task<IActionResult> Index()
        {
            var webMagazineContext = _context.SalesRecord.Include(s => s.Seller);
            return View(await webMagazineContext.ToListAsync());
        }

        // GET: SalesRecords/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SalesRecord == null)
            {
                return NotFound();
            }

            var salesRecord = await _context.SalesRecord
                .Include(s => s.Seller)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (salesRecord == null)
            {
                return NotFound();
            }

            return View(salesRecord);
        }

        // GET: SalesRecords/Create
        public IActionResult Create()
        {
            //propriedades, a primeira é a lista de vendedores
            var viewModel = new SalesRecordFormViewModel();
            //Carrega os vendedores do banco
            viewModel.Sellers = _context.Seller.ToList();
            return View(viewModel);
        }

        // POST: SalesRecords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Create(SalesRecordFormViewModel salesRecordFormViewModel)
        {
           
            _context.Add(salesRecordFormViewModel.SalesRecord);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }

        // GET: SalesRecords/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SalesRecord == null)
            {
                return NotFound();
            }

            var salesRecord = await _context.SalesRecord.FindAsync(id);
            if (salesRecord == null)
            {
                return NotFound();
            }

            //propriedades, a primeira é a lista de vendedores
            var viewModel = new SalesRecordFormViewModel();
            //popula com o sale record que se deseja editar
            viewModel.SalesRecord = salesRecord;
            //Carrega os vendedores do banco
            viewModel.Sellers = _context.Seller.ToList();
            return View(viewModel);
        }

        // POST: SalesRecords/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,Price,Status,SellerId")] SalesRecord salesRecord)
        {
            if (id != salesRecord.Id)
            {
                return NotFound();
            }

            try
            {
                _context.Update(salesRecord);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalesRecordExists(salesRecord.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        
        }

        // GET: SalesRecords/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SalesRecord == null)
            {
                return NotFound();
            }

            var salesRecord = await _context.SalesRecord
                .Include(s => s.Seller)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (salesRecord == null)
            {
                return NotFound();
            }

            return View(salesRecord);
        }

        // POST: SalesRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SalesRecord == null)
            {
                return Problem("Entity set 'WebMagazineContext.SalesRecord'  is null.");
            }
            var salesRecord = await _context.SalesRecord.FindAsync(id);
            if (salesRecord != null)
            {
                _context.SalesRecord.Remove(salesRecord);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalesRecordExists(int id)
        {
            return (_context.SalesRecord?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
