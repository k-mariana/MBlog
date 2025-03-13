using MBlog.API.Models.Domain;

namespace MBlog.API.Repositories;

public interface IArticleRepository
{
    Task<Article> CreateAsync(Article article);
    Task<List<Article>> GetAllAsync(string? filterOn = null, string? filterQuery = null, 
        string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000);
    Task<Article?> GetByIdAsync(Guid id);
    Task<Article?> UpdateAsync(Guid id, Article article);
    Task<Article?> DeleteAsync(Guid id);
}
