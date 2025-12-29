using CareerConnect.Application.DTOs.Common;

namespace CareerConnect.Application.Interfaces;

public interface IStatisticsService
{
    Task<StatisticsDto> GetStatisticsAsync(CancellationToken cancellationToken = default);
}
