using SupleStore.DTOs;

namespace SupleStore.Services
{
    public interface IProductoService
    {
        Task<IEnumerable<ProductosDto>> Get();
        Task<ProductosDto> GetById(int id);
        Task<ProductosDto> Add(ProductoInsertDto productoInsertDto);
        Task<ProductosDto> Update(int id, ProductoUpdateDto productoUpdateDto);
        Task<ProductosDto> Delete(int id);

    }
}
