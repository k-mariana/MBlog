namespace MBlog.API.Models.Domain;

public class Article
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public Guid UserId { get; set; }
    //public Guid CreatedBy { get; set; }
    //public Guid UpdateBy { get; set; }
    public DateTime CreatedTime { get; set; }
    public DateTime? UpdatedTime { get; set; }
    public Guid GenreId { get; set; }

    //Navigation properties
    public User User { get; set; }
    public Genre Genre { get; set; }
}
