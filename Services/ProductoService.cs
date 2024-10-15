using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SupleStore.DTOs;
using SupleStore.Models;
using SupleStore.Repository;

namespace SupleStore.Services
{
    public class ProductoService : ICommonService<ProductosDto, ProductoInsertDto, ProductoUpdateDto>
    {
     
        private IRepository<Productos> _productosRepository;
        private IMapper _mapper;

        public ProductoService(Context context,
                IRepository<Productos> productosRepository,
                IMapper mapper)
        {
            
            _productosRepository = productosRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductosDto>> Get()
        {
            var productos = await _productosRepository.Get();

            return productos.Select(p => _mapper.Map<ProductosDto>(p));
        }

        public async Task<ProductosDto> GetById(int id)
        {
            var producto= await _productosRepository.GetById(id);

            if (producto != null)
            {
                var productoDto = _mapper.Map<ProductosDto>(producto);
                return productoDto;
            }
            return null;

        }
        public async Task<ProductosDto> Add(ProductoInsertDto productoInsertDto)
        {
            

            var Producto = _mapper.Map<Productos>(productoInsertDto);

            await _productosRepository.Add(Producto);
            await _productosRepository.Save();

            var productoDto = _mapper.Map<ProductosDto>(Producto);

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

                var productoDto = _mapper.Map<ProductosDto>(producto);

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

                var productoDto = _mapper.Map<ProductosDto>(producto);
                return productoDto;
            }

            return null;
            
        }
    }
}
