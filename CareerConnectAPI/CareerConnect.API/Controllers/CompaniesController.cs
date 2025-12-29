using CareerConnect.Application.DTOs.Company;
using CareerConnect.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CareerConnect.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompaniesController : ControllerBase
{
    private readonly ICompanyService _companyService;

    public CompaniesController(ICompanyService companyService)
    {
        _companyService = companyService;
    }

    [HttpGet]
    public async Task<IActionResult> Search([FromQuery] CompanySearchParams searchParams, CancellationToken cancellationToken)
    {
        var result = await _companyService.SearchAsync(searchParams, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var company = await _companyService.GetByIdAsync(id, cancellationToken);
        if (company == null)
            return NotFound();
        return Ok(company);
    }

    [HttpGet("top")]
    public async Task<IActionResult> GetTop([FromQuery] int count = 6, CancellationToken cancellationToken = default)
    {
        var companies = await _companyService.GetTopAsync(count, cancellationToken);
        return Ok(companies);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCompanyDto dto, CancellationToken cancellationToken)
    {
        var company = await _companyService.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = company.Id }, company);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateCompanyDto dto, CancellationToken cancellationToken)
    {
        try
        {
            var company = await _companyService.UpdateAsync(id, dto, cancellationToken);
            return Ok(company);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        try
        {
            await _companyService.DeleteAsync(id, cancellationToken);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
