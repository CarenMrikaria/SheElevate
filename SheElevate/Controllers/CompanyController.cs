using Microsoft.AspNetCore.Mvc;
using SheElevate.Models;
using SheElevate.Data;
using Microsoft.EntityFrameworkCore;

namespace SheElevate.Controllers
{
    public class CompanyController : Controller
    {
        private readonly SheElevateContext _context;
        public CompanyController(SheElevateContext context)
        {
            _context = context;
        }
        public IActionResult AddCompany()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCompany(Company company)
        {
            if (ModelState.IsValid)
            {
                _context.CompanyTable.Add(company);
                await _context.SaveChangesAsync();
                return RedirectToAction("CompanyList");
            }
            return View(company);
        }
        public async Task<IActionResult> CompanyList()
        {
            var companies = await _context.CompanyTable.ToListAsync();
            return View(companies);
        }
        public IActionResult Update(int id)
        {
            var company = _context.CompanyTable.FirstOrDefault(c => c.CompanyID == id);
            if (company == null)
            {
                return NotFound();
            }
            return View(company);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, Company updatedCompany)
        {
            if (ModelState.IsValid)
            {
                var company = _context.CompanyTable.FirstOrDefault(c => c.CompanyID == id);
                if (company != null)
                {
                    company.CompanyName = updatedCompany.CompanyName;
                    company.IndustryType = updatedCompany.IndustryType;
                    company.EstablishedDate = updatedCompany.EstablishedDate;
                    company.Review = updatedCompany.Review;

                    _context.SaveChanges();
                    return RedirectToAction(nameof(CompanyList));
                }
            }
            return View(updatedCompany);
        }
    }
}
