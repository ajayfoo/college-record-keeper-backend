using CRK.Data;
using CRK.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRK.Controllers;

[ApiController, Authorize]
[Route("[controller]")]
public class CompanyController(CollegeDbContext context) : ControllerBase
{
    private readonly CollegeDbContext _context = context;

    [HttpGet("{id}")]
    public ActionResult<Company> GetCompany(Guid id)
    {
        Company? company = _context.Companies.Find(id);
        return (company == null) ? NotFound() : company;
    }

    [HttpPost]
    public async Task<ActionResult<Company>> AddCompany(Company company)
    {
        _ = _context.Companies.Add(company);
        _ = await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(AddCompany), new { id = company.Id }, company);
    }
}
