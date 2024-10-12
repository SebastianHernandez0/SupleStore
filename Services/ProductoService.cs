using Microsoft.EntityFrameworkCore;
using SupleStore.DTOs;
using SupleStore.Models;
using SupleStore.Repository;

namespace SupleStore.Services
{
    public class ProductoService : ICommonService<ProductosDto, ProductoInsertDto, ProductoUpdateDto>
    {
     
        private IRepository<Productos> _productosRepository;

        public ProductoService(Context context,
                IRepository<Productos> productosRepository)
        {
            
            _productosRepository = productosRepository;
        }

        public async Task<IEnumerable<ProductosDto>> Get()
        {
            var productos = await _productosRepository.Get();

            return productos.Select(p => new ProductosDto()
            {
                Id = p.Id,
                Name = p.Name,
                Image = p.Image,
                precio = p.precio,
                CategoryId = p.CategoryId,
                CategoryName = p.Categorias.CategoryName 
            });
        }

        public async Task<ProductosDto> GetById(int id)
        {
            var producto= await _productosRepository.GetById(id);

            if (producto != null)
            {
                var productoDto = new ProductosDto
                {
                    Id = producto.Id,
                    Name = producto.Name,
                    Image = producto.Image,
                    precio = producto.precio,
                    CategoryId = producto.CategoryId,
                    CategoryName = producto.Categorias.CategoryName
                };
                return productoDto;
            }
            return null;

        }
        public async Task<ProductosDto> Add(ProductoInsertDto productoInsertDto)
        {
            

            var Producto = new Productos
            {
                Name = productoInsertDto.Name,
                Image = productoInsertDto.Image,
                precio = productoInsertDto.precio,
                CategoryId = productoInsertDto.CategoryId
            };
            await _productosRepository.Add(Producto);
            await _productosRepository.Save();

            var productoDto = new ProductosDto
            {
                Id = Producto.Id,
                Name = Producto.Name,
                Image = Producto.Image,
                precio = Producto.precio,
                CategoryId = Producto.CategoryId

            };

            return productoDto;
        }
        public async Task<ProductosDto> Update(int id, ProductoUpdateDto productoUpdateDto)
        {
            var producto = await _productosRepository.GetById(id);

            if (producto != null)
            {
                producto.Name = productoUpdateDto.Name;
                producto.Image = productoUpdateDto.Image;
                producto.precio = productoUpdateDto.precio;
                producto.CategoryId = productoUpdateDto.CategoryId;

                _productosRepository.Update(producto);
                await _productosRepository.Save();

                var productoDto = new ProductosDto
                {
                    Id = producto.Id,
                    Name = producto.Name,
                    Image = producto.Image,
                    precio = producto.precio,
                    CategoryId = producto.CategoryId
                };

                return productoDto;
            }

            
            return null;
        }
        public async Task<ProductosDto> Delete(int id)
        {
            var producto = await _productosRepository.GetById(id);
            if (producto != null)
            {
                _productosRepository.Delete(producto);
                await _productosRepository.Save();

                var productoDto = new ProductosDto
                {
                    Id = producto.Id,
                    Name = producto.Name,
                    Image = producto.Image,
                    precio = producto.precio,
                    CategoryId = producto.CategoryId,
                    CategoryName= producto.Categorias.CategoryName
                };
                return productoDto;
            }

            return null;
            
        }
    }
}
