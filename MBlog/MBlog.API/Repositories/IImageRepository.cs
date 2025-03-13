using MBlog.API.Models.Domain;

namespace MBlog.API.Repositories;

public interface IImageRepository
{
    Task<Image> Upload(Image image);
}
