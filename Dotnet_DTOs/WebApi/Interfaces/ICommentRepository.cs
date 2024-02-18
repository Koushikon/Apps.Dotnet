using WebApi.Dtos.Comment;
using WebApi.Models;

namespace WebApi.Interfaces
{
    public interface ICommentRepository
    {
        Task<int> CreateAsync(Comment comment);
        Task<int?> DeleteAsync(int id);
        Task<List<Comment>> GetAllAsync();
        Task<Comment?> GetByIdAsync(int id);
        Task<int?> UpdateAsync(int id, CommentFormDto cfDto);
    }
}