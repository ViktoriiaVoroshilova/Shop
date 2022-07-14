using DataAccess.EF.DataAccess;
using DataAccess.EF.Models;

namespace DataAccess.EF;

public class InitialData
{
    public void Create(CategoryContext context)
    {
        if (context.Categories.ToArray().Length > 0)
        {
            return;
        }

        var categories = new List<Category>
        {
            new()
            {
                Id = 1,
                Name = "Books",
                Items = new List<Item>
                {
                    new() { Id = 1, Name = "Война и мир", Price = 1999.99 },
                    new() { Id = 2, Name = "Преступление и наказание", Price = 1599.99 },
                    new() { Id = 3, Name = "Колобок", Price = 60.0 },
                    new() { Id = 4, Name = "Математический Анализ", Price = 549.99 },
                    new() { Id = 5, Name = "Сказки народов мира", Price = 1949.99 },
                    new() { Id = 6, Name = "Сказки народов мираю Подарочное издание", Price = 5999.99 }
                }
            },
            new()
            {
                Id = 2,
                Name = "Shoes",
                Items = new List<Item>
                {
                    new() { Id = 7, Name = "Ecco", Price = 13000 },
                    new() { Id = 8, Name = "Officine Creative", Price = 49000 },
                    new() { Id = 9, Name = "Prada", Price = 190000 }
                }
            },
            new()
            {
                Id = 3,
                Name = "Sport",
                Items = Array.Empty<Item>()
            }
        };

        context.Categories.AddRange(categories);
        context.SaveChanges();
    }
}