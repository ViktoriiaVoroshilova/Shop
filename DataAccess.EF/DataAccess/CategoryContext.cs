using DataAccess.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EF.DataAccess;

public class CategoryContext : DbContext
{
    public CategoryContext(DbContextOptions<CategoryContext> options) : base(options) { }

    public DbSet<Category> Categories { get; set; } = null!;

    public DbSet<Item> Items { get; set; } = null!;
}