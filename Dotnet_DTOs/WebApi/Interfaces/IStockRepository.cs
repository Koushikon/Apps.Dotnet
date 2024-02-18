using WebApi.Dtos.Stock;
using WebApi.Helpers;
using WebApi.Models;

namespace WebApi.Interfaces;

public interface IStockRepository
{
    Task<int> CreateAsync(Stock stock);
    Task<int?> DeleteAsync(int id);
    Task<List<Stock>> GetAllAsync(QueryObject query);
    Task<Stock?> GetByIdAsync(int id);
    Task<bool> StockExists(int id);
    Task<int?> UpdateAsync(int id, StockRequestFormDto stock);
}