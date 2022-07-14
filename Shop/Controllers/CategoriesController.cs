using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Models;

namespace Shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoryContext _context;

        public CategoriesController(CategoryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<IEnumerable<Item>>> GetCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return category.Items.ToArray();
        }

        [HttpGet("{id:int}/items")]
        public async Task<ActionResult<IEnumerable<Item>>> GetCategoryItems(int id, [FromQuery] int page, [FromQuery] int limit)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return category.Items.Skip(page * limit).Take(limit).ToArray();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutCategory(int id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        [HttpPut("{id:int}/items/{itemId:int}")]
        public async Task<ActionResult<Category>> PutCategory(int id, int itemId, Item item)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null || itemId != item.Id)
            {
                return NotFound();
            }

            var updatedItem = category.Items.SingleOrDefault(i => i.Id == itemId);

            if (updatedItem == null)
            {
                return BadRequest();
            }

            updatedItem.Name = item.Name;
            _context.Entry(category.Items).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new { id = category.Id }, category);
        }

        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        { 
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetCategory", new { id = category.Id }, category);
        }

        [HttpPost("{id:int}/items")]
        public async Task<ActionResult<Category>> PostCategoryItem(int id, Item item)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            category.Items.Add(item);
            return CreatedAtAction("GetCategory", new { id = category.Id }, category);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id:int}/items/{itemId:int}")]
        public async Task<ActionResult<Category>> DeleteCategoryItem(int id, int itemId)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            category.Items = category.Items.Where(i => i.Id != itemId).ToArray();
            _context.Entry(category.Items).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new { id = category.Id }, category);
        }

        private bool CategoryExists(int id)
        {
            return (_context.Categories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
