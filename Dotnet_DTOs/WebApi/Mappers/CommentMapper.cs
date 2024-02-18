using WebApi.Dtos.Comment;
using WebApi.Models;

namespace WebApi.Mappers;

public static class CommentMapper
{
    
    public static CommentDto ToCommentDto(this Comment comment)
    {
        return new CommentDto
        {
            Id = comment.Id,
            Title = comment.Title,
            Content = comment.Content,
            CreatedOn = comment.CreatedOn,
            StockId = comment.StockId
        };
    }

    public static Comment ToCommentFromCommentFormDto(this CommentFormDto createComment, int stockId)
    {
        return new Comment
        {
            Title = createComment.Title,
            Content = createComment.Content,
            StockId = stockId
        };
    }
}