using WebApi.Contracts;

namespace WebApi.Services.Abstraction;

public interface IOrderService
{
    Task<OrderDto> CreateAsync(OrderForCreateDto orderCreateDto, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<OrderDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<OrderDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task UpdateAsync(Guid id, OrderForUpdateDto orderUpdateDto, CancellationToken cancellationToken = default);
}