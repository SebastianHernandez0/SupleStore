using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SupleStore.DTOs;
using SupleStore.Models;
using SupleStore.Services;

namespace SupleStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private Context _context;
        private IValidator<ProductoInsertDto> _productoInsertValidator;
        private IValidator<ProductoUpdateDto> _productoUpdateValidator;
        private ICommonService<ProductosDto,ProductoInsertDto,ProductoUpdateDto> _productoService;

        public ProductosController(Context context,
            IValidator<ProductoInsertDto> productoInsertValidator,
            IValidator<ProductoUpdateDto> productoUpdateValidator,
            [FromKeyedServices("productService")]ICommonService<ProductosDto, ProductoInsertDto, ProductoUpdateDto> productoService )
        {
            _context = context;
            _productoInsertValidator = productoInsertValidator;
            _productoUpdateValidator = productoUpdateValidator;
            _productoService = productoService;
        }

        [HttpGet]

        public async Task<IEnumerable<ProductosDto>> Get()
        {
           return await _productoService.Get();
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<ProductosDto>> GetById(int id)
        {
            var producto = await _productoService.GetById(id);
            
            return producto == null ? NotFound() : Ok(producto);

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
            if (productoExistente != null)
            {
                return Conflict(new { message = "Ya existe un producto con ese nombre" });
            }

            var productoDto = await _productoService.Add(productoInsertDto);

            return CreatedAtAction(nameof(GetById), new { id = productoDto.Id }, productoDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProductosDto>> Update(int id, ProductoUpdateDto productoUpdateDto)
        {
            var validationResult = await _productoUpdateValidator.ValidateAsync(productoUpdateDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var productoDto = await _productoService.Update(id, productoUpdateDto);

            return productoDto == null ? NotFound() : Ok(productoDto);
        }
        [HttpDelete("{id}")]

        public async Task<ActionResult<ProductosDto>> Delete(int id)
        {
            var productoDto = await _productoService.Delete(id);

            return productoDto == null ? NotFound() : Ok(productoDto);

        }
    }

}