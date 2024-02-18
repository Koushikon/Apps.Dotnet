using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Dtos.Comment;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly ApplicationDBContext _context;

    public CommentRepository(ApplicationDBContext context)
    {
        _context = context;
    }


    public async Task<int> CreateAsync(Comment comment)
    {
        await _context.Comments.AddAsync(comment);
        var dataInserted = await _context.SaveChangesAsync();
        return dataInserted;
    }


    public async Task<int?> UpdateAsync(int id, CommentFormDto cfDto)
    {
        var existingComment = await _context.Comments.FindAsync(id);

        if (existingComment == null)
            return null;

        existingComment.Title = cfDto.Title;
        existingComment.Content = cfDto.Content;

        var dataInserted = await _context.SaveChangesAsync();
        return dataInserted;
    }


    public async Task<List<Comment>> GetAllAsync()
    {
        return await _context.Comments.ToListAsync();
    }


    public async Task<Comment?> GetByIdAsync(int id)
    {
        return await _context.Comments.FindAsync(id);
    }


    public async Task<int?> DeleteAsync(int id)
    {
        var result = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);

        if (result == null)
            return null;

        _context.Comments.Remove(result);
        var dataDeleted = await _context.SaveChangesAsync();
        return dataDeleted;
    }
}