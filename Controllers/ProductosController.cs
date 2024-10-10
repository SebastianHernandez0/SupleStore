using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SupleStore.DTOs;
using SupleStore.Models;

namespace SupleStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private Context _context;

        public ProductosController(Context context)
        {
            _context = context;
        }

        [HttpGet]

        public async Task<IEnumerable<ProductosDto>> Get()
        {
            return await _context.Productos.Select(p=> new ProductosDto
            {
                Id = p.Id,
                Name = p.Name,
                Image = p.Image,
                precio = p.precio,
                CategoryId = p.CategoryId,
                CategoryName = p.Categorias.CategoryName
            }).ToListAsync();
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<ProductosDto>> GetById(int id)
        {
            var producto = await _context.Productos.Include(p => p.Categorias).FirstOrDefaultAsync(p => p.Id == id);

            if (producto == null)
            {
                return NotFound();
            }
            var productoDto = new ProductosDto
            {
                Id = producto.Id,
                Name = producto.Name,
                Image = producto.Image,
                precio = producto.precio,
                CategoryId = producto.CategoryId,
                CategoryName = producto.Categorias.CategoryName
            };
            return Ok(productoDto);
        }

        [HttpPost]

        public async Task<ActionResult<ProductosDto>> Add(ProductoInsertDto productoInsertDto)
        {
            var Producto = new Productos
            {
                Name = productoInsertDto.Name,
                Image = productoInsertDto.Image,
                precio = productoInsertDto.precio,
                CategoryId = productoInsertDto.CategoryId
            };
            await _context.Productos.AddAsync(Producto);
            await _context.SaveChangesAsync();

            var productoDto = new ProductosDto
            {
                Id = Producto.Id,
                Name = Producto.Name,
                Image = Producto.Image,
                precio = Producto.precio,
                CategoryId = Producto.CategoryId

            };
            return CreatedAtAction(nameof(GetById), new { id = Producto.Id }, productoDto);
        }
    }
}
