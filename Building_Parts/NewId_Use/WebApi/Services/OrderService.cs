using MassTransit;
using WebApi.Contracts;
using WebApi.Data;
using WebApi.Data.Models;
using WebApi.Services.Abstraction;

namespace WebApi.Services;

public class OrderService(IUnitOfWork unitOfWork) : IOrderService
{
    public async Task<OrderDto> CreateAsync(OrderForCreateDto orderCreateDto, CancellationToken cancellationToken = default)
    {
        var order = new Order
        {
            // Id = Guid.NewGuid(), // To Generate Guid
            Id = NewId.NextSequentialGuid(), // To Generate NewId using NewId Library
            CustomerName = orderCreateDto.CustomerName,
            Products = orderCreateDto.Products,
            TotalAmount = orderCreateDto.TotalAmount
        };

        unitOfWork.Order.Insert(order);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new OrderDto
        {
            Id = order.Id,
            CustomerName = order.CustomerName,
            Products = order.Products,
            TotalAmount = order.TotalAmount
        };
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Order order = await unitOfWork.Order.GetByIdAsync(id, cancellationToken)
            ?? throw new Exception("Order not found!");

        unitOfWork.Order.Remove(order);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<OrderDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        IEnumerable<Order> Orders = await unitOfWork.Order.GetAllAsync(cancellationToken);
        var ordersDto = new List<OrderDto>();

        foreach (var item in Orders)
        {
            ordersDto.Add(new OrderDto
            {
                Id = item.Id,
                CustomerName = item.CustomerName,
                Products = item.Products,
                TotalAmount = item.TotalAmount
            });
        }

        return ordersDto;
    }

    public async Task<OrderDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Order order = await unitOfWork.Order.GetByIdAsync(id, cancellationToken)
            ?? throw new Exception("Order not found!");

        return new OrderDto
        {
            Id = order.Id,
            CustomerName = order.CustomerName,
            Products = order.Products,
            TotalAmount = order.TotalAmount
        };
    }

    public async Task UpdateAsync(Guid id, OrderForUpdateDto orderUpdateDto, CancellationToken cancellationToken = default)
    {
        Order order = await unitOfWork.Order.GetByIdAsync(id, cancellationToken)
            ?? throw new Exception("Order not found!");

        order.CustomerName = orderUpdateDto.CustomerName;
        order.Products = orderUpdateDto.Products;
        order.TotalAmount = orderUpdateDto.TotalAmount;

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}