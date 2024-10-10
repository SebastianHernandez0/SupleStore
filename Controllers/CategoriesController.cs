using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SupleStore.DTOs;
using SupleStore.Models;

namespace SupleStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private Context _context;

        public CategoriesController(Context context)
        {
            _context = context;
        }

        [HttpGet]

        public async Task<IEnumerable<CategorysDto>> Get()
        {
            return await _context.Categorias.Select(c => new CategorysDto
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName
            }).ToListAsync();

        }

        [HttpPost]

        public async Task<ActionResult<CategorysDto>> Add(CategoryInsertDto categoryInsertDto)
        {
            var category = new Categorias
            {
                CategoryName = categoryInsertDto.CategoryName
            };

            await _context.Categorias.AddAsync(category);
            await _context.SaveChangesAsync();

            var categoryDto = new CategorysDto
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName
            };

            return Ok(categoryDto);
        }

        [HttpPut("{id}")]

        public async Task<ActionResult<CategorysDto>> Update(int id, CategoryUpdateDto categoryUpdateDto)
        {
            var category = await _context.Categorias.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            category.CategoryName = categoryUpdateDto.CategoryName;
            await _context.SaveChangesAsync();

            var categoryDto = new CategorysDto
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName
            };

            return Ok(categoryDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var category = await _context.Categorias.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            _context.Categorias.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}