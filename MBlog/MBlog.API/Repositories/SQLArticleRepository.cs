using MBlog.API.Data;
using MBlog.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace MBlog.API.Repositories;

public class SQLArticleRepository : IArticleRepository
{
    private readonly MBlogDbContext dbContext;

    public SQLArticleRepository(MBlogDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public async Task<Article> CreateAsync(Article article)
    {
        await dbContext.Articles.AddAsync(article);
        await dbContext.SaveChangesAsync();
        return article;
    }

    public async Task<Article?> DeleteAsync(Guid id)
    {
        var existingArticle = await dbContext.Articles.FirstOrDefaultAsync(x => x.Id == id);
        if(existingArticle == null)
        {
            return null;
        }

        dbContext.Articles.Remove(existingArticle);
        await dbContext.SaveChangesAsync();
        return existingArticle;
    }

    public async Task<List<Article>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
        string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000)
    {
        var articles = dbContext.Articles.Include("User").Include("Genre").AsQueryable();

        // Filtering
        if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
        {
            if (filterOn.Equals("Title", StringComparison.OrdinalIgnoreCase))
            {
                articles = articles.Where(x => x.Title.Contains(filterQuery));
            }
        }

        // Sorting
        if(string.IsNullOrWhiteSpace(sortBy) == false)
        {
            if (sortBy.Equals("Title", StringComparison.OrdinalIgnoreCase))
            {
                articles = isAscending ? articles.OrderBy(x => x.Title) : articles.OrderByDescending(x => x.Title);
            }
            else if (sortBy.Equals("CreatedTime", StringComparison.OrdinalIgnoreCase))
            {
                articles = isAscending ? articles.OrderBy(x => x.CreatedTime) : articles.OrderByDescending(x => x.CreatedTime);
            }
        }

        // Pagination
        var skipResults = (pageNumber - 1) * pageSize;

        return await articles.Skip(skipResults).Take(pageSize).ToListAsync();
        //return await dbContext.Articles.Include("User").Include("Genre").ToListAsync();
    }

    public async Task<Article?> GetByIdAsync(Guid id)
    {
        return await dbContext.Articles
            .Include("User")
            .Include("Genre")
            .FirstOrDefaultAsync(x => id == x.Id);
    }

    public async Task<Article?> UpdateAsync(Guid id, Article article)
    {
        var existingArticle = await dbContext.Articles.FirstOrDefaultAsync(x => x.Id == id);
        if(existingArticle == null)
        {
            return null;
        };

        existingArticle.Title = article.Title;
        existingArticle.Content = article.Content;
        existingArticle.CreatedTime = article.CreatedTime;
        existingArticle.UpdatedTime = article.UpdatedTime;
        existingArticle.UserId = article.UserId;
        existingArticle.GenreId = article.GenreId;

        await dbContext.SaveChangesAsync();
        return existingArticle;
    }
}
