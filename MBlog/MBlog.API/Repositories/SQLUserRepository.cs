using MBlog.API.Data;
using MBlog.API.Models.Domain;
using MBlog.API.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace MBlog.API.Repositories;

public class SQLUserRepository : IUserRepository
{
    private readonly MBlogDbContext dbContext;

    public SQLUserRepository(MBlogDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public async Task<List<User>> GetAllAsync()
    {
        return await dbContext.Users.ToListAsync();
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<User> CreateAsync(User user)
    {
        await dbContext.Users.AddAsync(user);
        await dbContext.SaveChangesAsync();
        return user;
    }

    public async Task<User?> UpdateAsync(Guid id, User user)
    {
        var existingUser = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        if (existingUser == null)
        {
            return null;
        }

        // Map Dto to domain model
        existingUser.FirstName = user.FirstName;
        existingUser.LastName = user.LastName;

        await dbContext.SaveChangesAsync();
        return existingUser;
    }

    public async Task<User?> DeleteAsync(Guid id)
    {
        var existingUser = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        if (existingUser == null)
        {
            return null;
        }

        // Delete user
        dbContext.Users.Remove(existingUser);
        await dbContext.SaveChangesAsync();
        return existingUser;
    }
}
