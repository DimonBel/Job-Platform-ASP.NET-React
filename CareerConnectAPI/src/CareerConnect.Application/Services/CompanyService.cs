using CareerConnect.Application.DTOs.Common;
using CareerConnect.Application.DTOs.Company;
using CareerConnect.Application.DTOs.Job;
using CareerConnect.Application.Interfaces;
using CareerConnect.Domain.Entities;
using CareerConnect.Domain.Interfaces;

namespace CareerConnect.Application.Services;

public class CompanyService : ICompanyService
{
    private readonly IUnitOfWork _unitOfWork;

    public CompanyService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<CompanyDetailDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var company = await _unitOfWork.Companies.GetByIdWithJobsAsync(id, cancellationToken);
        if (company == null) return null;

        return MapToDetailDto(company);
    }

    public async Task<PagedResult<CompanyDto>> SearchAsync(CompanySearchParams searchParams, CancellationToken cancellationToken = default)
    {
        var (companies, totalCount) = await _unitOfWork.Companies.SearchCompaniesAsync(
            searchParams.Query,
            searchParams.Industry,
            searchParams.Location,
            searchParams.Page,
            searchParams.PageSize,
            cancellationToken);

        return new PagedResult<CompanyDto>
        {
            Items = companies.Select(MapToDto).ToList(),
            TotalCount = totalCount,
            Page = searchParams.Page,
            PageSize = searchParams.PageSize
        };
    }

    public async Task<IReadOnlyList<CompanyDto>> GetTopAsync(int count = 6, CancellationToken cancellationToken = default)
    {
        var companies = await _unitOfWork.Companies.GetTopCompaniesAsync(count, cancellationToken);
        return companies.Select(MapToDto).ToList();
    }

    public async Task<CompanyDto> CreateAsync(CreateCompanyDto dto, CancellationToken cancellationToken = default)
    {
        var company = new Company
        {
            Name = dto.Name,
            Description = dto.Description,
            Industry = dto.Industry,
            Location = dto.Location,
            Logo = dto.Logo,
            Website = dto.Website,
            Size = dto.Size,
            FoundedYear = dto.FoundedYear
        };

        var created = await _unitOfWork.Companies.AddAsync(company, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return MapToDto(created);
    }

    public async Task<CompanyDto> UpdateAsync(int id, CreateCompanyDto dto, CancellationToken cancellationToken = default)
    {
        var company = await _unitOfWork.Companies.GetByIdAsync(id, cancellationToken);
        if (company == null) throw new KeyNotFoundException($"Company with ID {id} not found");

        company.Name = dto.Name;
        company.Description = dto.Description;
        company.Industry = dto.Industry;
        company.Location = dto.Location;
        company.Logo = dto.Logo;
        company.Website = dto.Website;
        company.Size = dto.Size;
        company.FoundedYear = dto.FoundedYear;
        company.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.Companies.UpdateAsync(company, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return MapToDto(company);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var company = await _unitOfWork.Companies.GetByIdAsync(id, cancellationToken);
        if (company == null) throw new KeyNotFoundException($"Company with ID {id} not found");

        await _unitOfWork.Companies.DeleteAsync(company, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private static CompanyDto MapToDto(Company company)
    {
        return new CompanyDto
        {
            Id = company.Id,
            Name = company.Name,
            Description = company.Description,
            Industry = company.Industry,
            Location = company.Location,
            Logo = company.Logo,
            Size = company.Size,
            Rating = company.Rating,
            ReviewCount = company.ReviewCount,
            OpenJobsCount = company.Jobs?.Count(j => j.IsActive) ?? 0,
            IsVerified = company.IsVerified
        };
    }

    private static CompanyDetailDto MapToDetailDto(Company company)
    {
        return new CompanyDetailDto
        {
            Id = company.Id,
            Name = company.Name,
            Description = company.Description,
            Industry = company.Industry,
            Location = company.Location,
            Logo = company.Logo,
            Website = company.Website,
            Size = company.Size,
            FoundedYear = company.FoundedYear,
            Rating = company.Rating,
            ReviewCount = company.ReviewCount,
            OpenJobsCount = company.Jobs?.Count(j => j.IsActive) ?? 0,
            IsVerified = company.IsVerified,
            RecentJobs = company.Jobs?
                .Where(j => j.IsActive)
                .OrderByDescending(j => j.PostedDate)
                .Take(5)
                .Select(j => new JobDto
                {
                    Id = j.Id,
                    Title = j.Title,
                    Company = company.Name,
                    CompanyLogo = company.Logo,
                    CompanyId = company.Id,
                    Location = j.Location,
                    Salary = j.Salary,
                    Type = j.JobType,
                    PostedAt = GetTimeAgo(j.PostedDate),
                    Featured = j.IsFeatured,
                    Tags = j.JobTags.Select(jt => jt.Tag.Name).ToList()
                }).ToList() ?? new List<JobDto>()
        };
    }

    private static string GetTimeAgo(DateTime date)
    {
        var timeSpan = DateTime.UtcNow - date;
        
        if (timeSpan.TotalDays >= 30)
            return $"{(int)(timeSpan.TotalDays / 30)} month{((int)(timeSpan.TotalDays / 30) > 1 ? "s" : "")} ago";
        if (timeSpan.TotalDays >= 7)
            return $"{(int)(timeSpan.TotalDays / 7)} week{((int)(timeSpan.TotalDays / 7) > 1 ? "s" : "")} ago";
        if (timeSpan.TotalDays >= 1)
            return $"{(int)timeSpan.TotalDays} day{((int)timeSpan.TotalDays > 1 ? "s" : "")} ago";
        if (timeSpan.TotalHours >= 1)
            return $"{(int)timeSpan.TotalHours} hour{((int)timeSpan.TotalHours > 1 ? "s" : "")} ago";
        return "Just now";
    }
}
