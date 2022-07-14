namespace Shop.Models;

public class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public IList<Item> Items { get; set; } = null!;
}