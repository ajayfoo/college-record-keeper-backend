using CRK.Data;
using CRK.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRK.Controllers;

[ApiController]
[Route("[controller]")]
public class CompanyController(CollegeDbContext context) : ControllerBase
{
    private readonly CollegeDbContext _context = context;

    [HttpGet("{id}")]
    public ActionResult<Company> GetCompany(Guid id)
    {
        Company? company = _context.Companies.Find(id);
        if (company == null)
        {
            return NotFound();
        }
        return company;
    }

    [HttpPost]
    public async Task<ActionResult<Company>> AddCompany(Company company)
    {
        _context.Companies.Add(company);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(AddCompany), new { id = company.Id }, company);
    }
}
