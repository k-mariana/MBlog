using MBlog.API.Data;
using MBlog.API.Models.Domain;

namespace MBlog.API.Repositories;

public class LocalImageRepository : IImageRepository
{
    private readonly IWebHostEnvironment webHostEnvironment;
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly MBlogDbContext dbContext;

    public LocalImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, MBlogDbContext dbContext)
    {
        this.webHostEnvironment = webHostEnvironment;
        this.httpContextAccessor = httpContextAccessor;
        this.dbContext = dbContext;
    }
    public async Task<Image> Upload(Image image)
    {
        var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", 
            $"{image.FileName}{image.FileExtension}");

        // Upload image to local path
        using var stream = new FileStream(localFilePath, FileMode.Create);
        await image.File.CopyToAsync(stream);

        // https://localhost:7019/images/image.jpg
        var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";
        image.FilePath = urlFilePath;

        // Add image to the Images table
        await dbContext.Images.AddAsync(image);
        await dbContext.SaveChangesAsync();

        return image;
    }
}
