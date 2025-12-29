using CareerConnect.Application.DTOs.Common;
using CareerConnect.Application.DTOs.Company;

namespace CareerConnect.Application.Interfaces;

public interface ICompanyService
{
    Task<CompanyDetailDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<PagedResult<CompanyDto>> SearchAsync(CompanySearchParams searchParams, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<CompanyDto>> GetTopAsync(int count = 6, CancellationToken cancellationToken = default);
    Task<CompanyDto> CreateAsync(CreateCompanyDto dto, CancellationToken cancellationToken = default);
    Task<CompanyDto> UpdateAsync(int id, CreateCompanyDto dto, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}
