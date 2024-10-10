using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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
        private IValidator<ProductoInsertDto> _productoInsertValidator;
        private IValidator<ProductoUpdateDto> _productoUpdateValidator;

        public ProductosController(Context context,
            IValidator<ProductoInsertDto> productoInsertValidator,
            IValidator<ProductoUpdateDto> productoUpdateValidator)
        {
            _context = context;
            _productoInsertValidator = productoInsertValidator;
            _productoUpdateValidator = productoUpdateValidator;
        }

        [HttpGet]

        public async Task<IEnumerable<ProductosDto>> Get()
        {
            return await _context.Productos.Select(p => new ProductosDto
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
            

            var validationResult= await _productoInsertValidator.ValidateAsync(productoInsertDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var productoExistente = await _context.Productos.FirstOrDefaultAsync(p => p.Name.ToLower() == productoInsertDto.Name.ToLower());
            if(productoExistente != null)
            {
                return Conflict(new { message = "Ya existe un producto con ese nombre" });
            }

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

        [HttpPut("{id}")]
        public async Task<ActionResult<ProductosDto>> Update(int id, ProductoUpdateDto productoUpdateDto)
        {
            var validationResult = await _productoUpdateValidator.ValidateAsync(productoUpdateDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
            {
                return NotFound();
            }

            producto.Name = productoUpdateDto.Name;
            producto.Image = productoUpdateDto.Image;
            producto.precio = productoUpdateDto.precio;
            producto.CategoryId = productoUpdateDto.CategoryId;

            await _context.SaveChangesAsync();

            var productoDto = new ProductosDto
            {
                Id = producto.Id,
                Name = producto.Name,
                Image = producto.Image,
                precio = producto.precio,
                CategoryId = producto.CategoryId
            };
            return Ok(productoDto);
        }
        [HttpDelete("{id}")]

        public async Task<ActionResult> Delete(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            _context.Remove(producto);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

}