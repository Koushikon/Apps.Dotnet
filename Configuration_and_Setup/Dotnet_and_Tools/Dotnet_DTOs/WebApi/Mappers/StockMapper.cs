using WebApi.Dtos.Stock;
using WebApi.Models;

namespace WebApi.Mappers;

public static class StockMapper
{
    public static StockDto ToStockDto(this Stock stock)
    {
        return new StockDto
        {
            Id = stock.Id,
            Symbol = stock.Symbol,
            CompanyName = stock.CompanyName,
            Purchase = stock.Purchase,
            LastDiv = stock.LastDiv,
            Industry = stock.Industry,
            MarketCap = stock.MarketCap,
            Comments = stock.Comments.Select(c => c.ToCommentDto()).ToList()
        };
    }

    public static Stock ToStockFromStockRequestFormDto(this StockRequestFormDto srfDto)
    {
        return new Stock
        {
            Symbol = srfDto.Symbol,
            CompanyName = srfDto.CompanyName,
            Purchase = srfDto.Purchase,
            LastDiv = srfDto.LastDiv,
            Industry = srfDto.Industry,
            MarketCap = srfDto.MarketCap
        };
    }
}