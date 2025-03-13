using MBlog.API.Models.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MBlog.API.Data;

public class MBlogDbContext: DbContext
{
    public MBlogDbContext(DbContextOptions<MBlogDbContext> dbContextOptions): base(dbContextOptions)
    {
        
    }

    public DbSet<Article> Articles { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<User> Users { get; set; }

    public DbSet<Image> Images { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var genres = new List<Genre>()
        {
            new Genre()
            {
                Id = Guid.Parse("2f5f840e-ebcb-422b-805b-bc908b66269b"),
                Name = "Travel"
            },
            new Genre()
            {
                Id = Guid.Parse("3e9e946f-e961-4f26-bd16-6ae3c7d8b799"),
                Name = "Film"
            },
            new Genre()
            {
                Id = Guid.Parse("1543228b-9f94-4b11-b7cd-5b9990bec802"),
                Name = "Programming"
            }
        };

        modelBuilder.Entity<Genre>().HasData(genres);

        var users = new List<User>()
        {
            new User()
            {
                Id = Guid.Parse("f832050f-5325-468a-bd97-dc86d64d5495"),
                FirstName = "Olga",
                LastName = "Tokar"
            },
            new User()
            {
                Id = Guid.Parse("cb37f95d-aee6-48c5-8624-d730c2806c34"),
                FirstName = "John",
                LastName = "Smith"
            },
            new User()
            {
                Id = Guid.Parse("a05d6a71-c602-4a50-8535-552c26daab92"),
                FirstName = "Andrew",
                LastName = "Miller"
            },
            new User()
            {
                Id = Guid.Parse("3f6f6389-8509-4987-b109-e1b895ad686c"),
                FirstName = "Alina",
                LastName = "Fedak"
            },
            new User()
            {
                Id = Guid.Parse("38ff1a54-3661-4923-90b2-3283b57b0613"),
                FirstName = "Anna",
                LastName = "Boyko"
            }
        };

        modelBuilder.Entity<User>().HasData(users);
    }

}
