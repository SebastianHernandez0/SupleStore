using SupleStore.DTOs;

namespace SupleStore.Services
{
    public interface ICommonService<T, TI, TU>
    {
        Task<IEnumerable<T>> Get();
        Task<T> GetById(int id);
        Task<T> Add(TI productoInsertDto);
        Task<T> Update(int id, TU productoUpdateDto);
        Task<T> Delete(int id);

    }
}
