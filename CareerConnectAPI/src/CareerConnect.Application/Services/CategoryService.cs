using CareerConnect.Application.DTOs.Category;
using CareerConnect.Application.Interfaces;
using CareerConnect.Domain.Entities;
using CareerConnect.Domain.Interfaces;

namespace CareerConnect.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<CategoryDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var category = await _unitOfWork.Categories.GetByIdAsync(id, cancellationToken);
        if (category == null) return null;

        var jobCount = await _unitOfWork.Jobs.CountAsync(j => j.CategoryId == id && j.IsActive, cancellationToken);

        return new CategoryDto
        {
            Id = category.Id,
            Title = category.Name,
            Description = category.Description,
            Icon = category.Icon,
            Color = category.Color,
            Count = jobCount
        };
    }

    public async Task<IReadOnlyList<CategoryDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var categories = await _unitOfWork.Categories.GetAllAsync(cancellationToken);
        
        var result = new List<CategoryDto>();
        foreach (var category in categories)
        {
            var jobCount = await _unitOfWork.Jobs.CountAsync(j => j.CategoryId == category.Id && j.IsActive, cancellationToken);
            result.Add(new CategoryDto
            {
                Id = category.Id,
                Title = category.Name,
                Description = category.Description,
                Icon = category.Icon,
                Color = category.Color,
                Count = jobCount
            });
        }

        return result;
    }

    public async Task<IReadOnlyList<CategoryDto>> GetWithJobCountAsync(CancellationToken cancellationToken = default)
    {
        var categories = await _unitOfWork.Categories.GetCategoriesWithJobCountAsync(cancellationToken);
        
        return categories.Select(c => new CategoryDto
        {
            Id = c.Id,
            Title = c.Name,
            Description = c.Description,
            Icon = c.Icon,
            Color = c.Color,
            Count = c.Jobs?.Count(j => j.IsActive) ?? 0
        }).ToList();
    }

    public async Task<CategoryDto> CreateAsync(CreateCategoryDto dto, CancellationToken cancellationToken = default)
    {
        var category = new Category
        {
            Name = dto.Name,
            Description = dto.Description,
            Icon = dto.Icon,
            Color = dto.Color
        };

        var created = await _unitOfWork.Categories.AddAsync(category, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new CategoryDto
        {
            Id = created.Id,
            Title = created.Name,
            Description = created.Description,
            Icon = created.Icon,
            Color = created.Color,
            Count = 0
        };
    }

    public async Task<CategoryDto> UpdateAsync(int id, CreateCategoryDto dto, CancellationToken cancellationToken = default)
    {
        var category = await _unitOfWork.Categories.GetByIdAsync(id, cancellationToken);
        if (category == null) throw new KeyNotFoundException($"Category with ID {id} not found");

        category.Name = dto.Name;
        category.Description = dto.Description;
        category.Icon = dto.Icon;
        category.Color = dto.Color;
        category.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.Categories.UpdateAsync(category, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var jobCount = await _unitOfWork.Jobs.CountAsync(j => j.CategoryId == id && j.IsActive, cancellationToken);

        return new CategoryDto
        {
            Id = category.Id,
            Title = category.Name,
            Description = category.Description,
            Icon = category.Icon,
            Color = category.Color,
            Count = jobCount
        };
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var category = await _unitOfWork.Categories.GetByIdAsync(id, cancellationToken);
        if (category == null) throw new KeyNotFoundException($"Category with ID {id} not found");

        await _unitOfWork.Categories.DeleteAsync(category, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
