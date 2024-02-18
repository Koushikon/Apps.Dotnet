using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Dtos.Stock;
using WebApi.Helpers;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Repositories;

public class StockRepository : IStockRepository
{
    private readonly ApplicationDBContext _context;

    public StockRepository(ApplicationDBContext context)
    {
        _context = context;
    }


    public async Task<int> CreateAsync(Stock stock)
    {
        await _context.Stocks.AddAsync(stock);
        var dataInserted = await _context.SaveChangesAsync();
        return dataInserted;
    }


    public async Task<int?> UpdateAsync(int id, StockRequestFormDto stock)
    {
        var existingStock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);

        if (existingStock == null)
            return null;

        existingStock.Symbol = stock.Symbol;
        existingStock.CompanyName = stock.CompanyName;
        existingStock.Purchase = stock.Purchase;
        existingStock.LastDiv = stock.LastDiv;
        existingStock.Industry = stock.Industry;
        existingStock.MarketCap = stock.MarketCap;

        var dataInserted = await _context.SaveChangesAsync();
        return dataInserted;
    }


    public async Task<List<Stock>> GetAllAsync(QueryObject query)
    {
        var stocks = _context.Stocks.Include(c => c.Comments).AsQueryable();

        if (!string.IsNullOrWhiteSpace(query.Symbol))
        {
            stocks = stocks.Where(s => s.Symbol.Contains(query.Symbol));
        }

        if (!string.IsNullOrWhiteSpace(query.CompanyName))
        {
            stocks = stocks.Where(s => s.CompanyName.Contains(query.CompanyName));
        }

        if (!string.IsNullOrWhiteSpace(query.SortBy))
        {
            if (query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
            {
                stocks = query.IsDecsending ? stocks.OrderByDescending(s => s.Symbol) : stocks.OrderBy(s => s.Symbol);
            }
        }

        var skipNumber = (query.PageNumber - 1) * query.PageSize;

        return await stocks.Skip(skipNumber).Take(query.PageSize).ToListAsync();
    }


    public async Task<Stock?> GetByIdAsync(int id)
    {
        return await _context.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(x => x.Id == id);
    }


    public async Task<bool> StockExists(int id)
    {
        return await _context.Stocks.AnyAsync(s => s.Id == id);
    }


    public async Task<int?> DeleteAsync(int id)
    {
        var result = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);

        if (result == null)
            return null;

        _context.Stocks.Remove(result);
        var dataDeleted = await _context.SaveChangesAsync();
        return dataDeleted;
    }
}