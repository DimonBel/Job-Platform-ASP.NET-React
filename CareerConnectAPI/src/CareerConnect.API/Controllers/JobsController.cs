using CareerConnect.Application.DTOs.Job;
using CareerConnect.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CareerConnect.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JobsController : ControllerBase
{
    private readonly IJobService _jobService;

    public JobsController(IJobService jobService)
    {
        _jobService = jobService;
    }

    [HttpGet]
    public async Task<IActionResult> Search([FromQuery] JobSearchParams searchParams, CancellationToken cancellationToken)
    {
        var result = await _jobService.SearchAsync(searchParams, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var job = await _jobService.GetByIdAsync(id, cancellationToken);
        if (job == null)
            return NotFound();
        return Ok(job);
    }

    [HttpGet("featured")]
    public async Task<IActionResult> GetFeatured([FromQuery] int count = 6, CancellationToken cancellationToken = default)
    {
        var jobs = await _jobService.GetFeaturedAsync(count, cancellationToken);
        return Ok(jobs);
    }

    [HttpGet("company/{companyId:int}")]
    public async Task<IActionResult> GetByCompany(int companyId, CancellationToken cancellationToken)
    {
        var jobs = await _jobService.GetByCompanyAsync(companyId, cancellationToken);
        return Ok(jobs);
    }

    [HttpGet("category/{categoryId:int}")]
    public async Task<IActionResult> GetByCategory(int categoryId, CancellationToken cancellationToken)
    {
        var jobs = await _jobService.GetByCategoryAsync(categoryId, cancellationToken);
        return Ok(jobs);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateJobDto dto, CancellationToken cancellationToken)
    {
        var job = await _jobService.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = job.Id }, job);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateJobDto dto, CancellationToken cancellationToken)
    {
        try
        {
            var job = await _jobService.UpdateAsync(id, dto, cancellationToken);
            return Ok(job);
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
            await _jobService.DeleteAsync(id, cancellationToken);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
