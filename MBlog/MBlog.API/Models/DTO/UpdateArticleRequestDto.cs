using System.ComponentModel.DataAnnotations;

namespace MBlog.API.Models.DTO;

public class UpdateArticleRequestDto
{
    [Required]
    [MinLength(2)]
    public string Title { get; set; }
    [Required]
    public string Content { get; set; }
    [Required]
    public Guid UserId { get; set; }
    //public Guid CreatedBy { get; set; }
    //public Guid UpdateBy { get; set; }
    [Required]
    public DateTime CreatedTime { get; set; }
    public DateTime? UpdatedTime { get; set; }
    [Required]
    public Guid GenreId { get; set; }
}
