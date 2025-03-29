using System.ComponentModel.DataAnnotations;

namespace WebApi.Dtos.Comment;

public class CommentFormDto
{
    [Required]
    [Length(5, 280, ErrorMessage = "Title must be between 5 to 280 characters.")]
    public string Title { get; set; } = string.Empty;

    [Required]
    [Length(5, 280, ErrorMessage = "Content must be between 5 to 280 characters.")]
    public string Content { get; set; } = string.Empty;
}