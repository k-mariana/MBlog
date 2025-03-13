using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MBlog.API.Data;

public class MBlogAuthDbContext : IdentityDbContext
{
    public MBlogAuthDbContext(DbContextOptions<MBlogAuthDbContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        var readerRoleId = "e5efeb29-7ca4-46f4-a8fb-7c9a51701bab";
        var writerRoleId = "300f7858-292c-499f-8e97-33a6e51c66e2";

        var roles = new List<IdentityRole>
        {
            new IdentityRole
            {
                Id = readerRoleId,
                ConcurrencyStamp = readerRoleId,
                Name = "Reader",
                NormalizedName = "Reader".ToUpper()
            },
            new IdentityRole
            {
                Id = writerRoleId,
                ConcurrencyStamp = writerRoleId,
                Name = "Writer",
                NormalizedName = "Writer".ToUpper()
            }
        };
        builder.Entity<IdentityRole>().HasData(roles);
    }

}
