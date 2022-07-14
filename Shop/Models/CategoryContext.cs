using Microsoft.EntityFrameworkCore;

namespace Shop.Models;

public class CategoryContext : DbContext
{
    public CategoryContext(DbContextOptions<CategoryContext> options)
        : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; } = null!;
}