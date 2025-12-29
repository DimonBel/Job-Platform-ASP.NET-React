using CareerConnect.Application.DTOs.Common;
using CareerConnect.Application.DTOs.Job;
using CareerConnect.Application.Interfaces;
using CareerConnect.Domain.Entities;
using CareerConnect.Domain.Interfaces;

namespace CareerConnect.Application.Services;

public class JobService : IJobService
{
    private readonly IUnitOfWork _unitOfWork;

    public JobService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<JobDetailDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var job = await _unitOfWork.Jobs.GetByIdWithDetailsAsync(id, cancellationToken);
        if (job == null) return null;

        return MapToDetailDto(job);
    }

    public async Task<PagedResult<JobDto>> SearchAsync(JobSearchParams searchParams, CancellationToken cancellationToken = default)
    {
        var (jobs, totalCount) = await _unitOfWork.Jobs.SearchJobsAsync(
            searchParams.Query,
            searchParams.Location,
            searchParams.JobType,
            searchParams.CategoryId,
            searchParams.CompanyId,
            searchParams.MinSalary,
            searchParams.MaxSalary,
            searchParams.Page,
            searchParams.PageSize,
            cancellationToken);

        return new PagedResult<JobDto>
        {
            Items = jobs.Select(MapToDto).ToList(),
            TotalCount = totalCount,
            Page = searchParams.Page,
            PageSize = searchParams.PageSize
        };
    }

    public async Task<IReadOnlyList<JobDto>> GetFeaturedAsync(int count = 6, CancellationToken cancellationToken = default)
    {
        var jobs = await _unitOfWork.Jobs.GetFeaturedJobsAsync(count, cancellationToken);
        return jobs.Select(MapToDto).ToList();
    }

    public async Task<IReadOnlyList<JobDto>> GetByCompanyAsync(int companyId, CancellationToken cancellationToken = default)
    {
        var jobs = await _unitOfWork.Jobs.GetJobsByCompanyAsync(companyId, cancellationToken);
        return jobs.Select(MapToDto).ToList();
    }

    public async Task<IReadOnlyList<JobDto>> GetByCategoryAsync(int categoryId, CancellationToken cancellationToken = default)
    {
        var jobs = await _unitOfWork.Jobs.GetJobsByCategoryAsync(categoryId, cancellationToken);
        return jobs.Select(MapToDto).ToList();
    }

    public async Task<JobDto> CreateAsync(CreateJobDto dto, CancellationToken cancellationToken = default)
    {
        var job = new Job
        {
            Title = dto.Title,
            Description = dto.Description,
            Location = dto.Location,
            Salary = dto.Salary,
            JobType = dto.JobType,
            ExperienceLevel = dto.ExperienceLevel,
            CompanyId = dto.CompanyId,
            CategoryId = dto.CategoryId,
            IsFeatured = dto.IsFeatured,
            ClosingDate = dto.ClosingDate,
            PostedDate = DateTime.UtcNow
        };

        // Add responsibilities
        foreach (var (resp, index) in dto.Responsibilities.Select((r, i) => (r, i)))
        {
            job.Responsibilities.Add(new JobResponsibility { Description = resp, Order = index });
        }

        // Add requirements
        foreach (var (req, index) in dto.Requirements.Select((r, i) => (r, i)))
        {
            job.Requirements.Add(new JobRequirement { Description = req, Order = index });
        }

        // Add benefits
        foreach (var (benefit, index) in dto.Benefits.Select((b, i) => (b, i)))
        {
            job.Benefits.Add(new JobBenefit { Description = benefit, Order = index });
        }

        // Add tags
        foreach (var tagName in dto.Tags)
        {
            var tag = await _unitOfWork.Tags.GetByNameAsync(tagName, cancellationToken);
            if (tag == null)
            {
                tag = await _unitOfWork.Tags.AddAsync(new Tag { Name = tagName }, cancellationToken);
            }
            job.JobTags.Add(new JobTag { Tag = tag });
        }

        var createdJob = await _unitOfWork.Jobs.AddAsync(job, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return MapToDto(createdJob);
    }

    public async Task<JobDto> UpdateAsync(int id, UpdateJobDto dto, CancellationToken cancellationToken = default)
    {
        var job = await _unitOfWork.Jobs.GetByIdWithDetailsAsync(id, cancellationToken);
        if (job == null) throw new KeyNotFoundException($"Job with ID {id} not found");

        job.Title = dto.Title;
        job.Description = dto.Description;
        job.Location = dto.Location;
        job.Salary = dto.Salary;
        job.JobType = dto.JobType;
        job.ExperienceLevel = dto.ExperienceLevel;
        job.CategoryId = dto.CategoryId;
        job.IsFeatured = dto.IsFeatured;
        job.IsActive = dto.IsActive;
        job.ClosingDate = dto.ClosingDate;
        job.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.Jobs.UpdateAsync(job, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return MapToDto(job);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var job = await _unitOfWork.Jobs.GetByIdAsync(id, cancellationToken);
        if (job == null) throw new KeyNotFoundException($"Job with ID {id} not found");

        await _unitOfWork.Jobs.DeleteAsync(job, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private static JobDto MapToDto(Job job)
    {
        var timeAgo = GetTimeAgo(job.PostedDate);
        
        return new JobDto
        {
            Id = job.Id,
            Title = job.Title,
            Company = job.Company?.Name ?? string.Empty,
            CompanyLogo = job.Company?.Logo,
            CompanyId = job.CompanyId,
            Location = job.Location,
            Salary = job.Salary,
            Type = job.JobType,
            PostedAt = timeAgo,
            Featured = job.IsFeatured,
            Tags = job.JobTags.Select(jt => jt.Tag.Name).ToList()
        };
    }

    private static JobDetailDto MapToDetailDto(Job job)
    {
        var timeAgo = GetTimeAgo(job.PostedDate);
        
        return new JobDetailDto
        {
            Id = job.Id,
            Title = job.Title,
            Description = job.Description,
            Company = job.Company?.Name ?? string.Empty,
            CompanyId = job.CompanyId,
            CompanyLogo = job.Company?.Logo,
            CompanyDescription = job.Company?.Description,
            CompanySize = job.Company?.Size,
            CompanyWebsite = job.Company?.Website,
            Location = job.Location,
            Salary = job.Salary,
            Type = job.JobType,
            ExperienceLevel = job.ExperienceLevel,
            PostedAt = timeAgo,
            ClosingDate = job.ClosingDate,
            Featured = job.IsFeatured,
            Applicants = job.Applications?.Count ?? 0,
            Category = job.Category?.Name,
            Tags = job.JobTags.Select(jt => jt.Tag.Name).ToList(),
            Responsibilities = job.Responsibilities.OrderBy(r => r.Order).Select(r => r.Description).ToList(),
            Requirements = job.Requirements.OrderBy(r => r.Order).Select(r => r.Description).ToList(),
            Benefits = job.Benefits.OrderBy(b => b.Order).Select(b => b.Description).ToList()
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
