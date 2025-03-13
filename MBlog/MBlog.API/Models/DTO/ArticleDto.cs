namespace MBlog.API.Models.DTO;

public class ArticleDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    
    //public Guid CreatedBy { get; set; }
    //public Guid UpdateBy { get; set; }
    public DateTime CreatedTime { get; set; }
    public DateTime? UpdatedTime { get; set; }
    

    public UserDto User { get; set; }
    public GenreDto Genre { get; set; }
}
