using Microsoft.EntityFrameworkCore;
using SupleStore.Models;

namespace SupleStore.Repository
{
    public class ProductRepository : IRepository<Productos>
    {
        private Context _context;

        public ProductRepository(Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Productos>> Get()
        {
            return await _context.Productos.Include(p => p.Categorias).ToListAsync();
        }

        public async Task<Productos> GetById(int id)
        {
            var producto = await _context.Productos.Include(p => p.Categorias).FirstOrDefaultAsync(p => p.Id == id);
            if (producto == null) {
                return null;
            }

            return producto;
        }
        public async Task Add(Productos entity)
        {
            await _context.Productos.AddAsync(entity);
        }

        public void Update(Productos entity)
        {
            _context.Productos.Attach(entity);
            _context.Productos.Entry(entity).State = EntityState.Modified;
            
        }

        public void Delete(Productos entity)
        {
            _context.Productos.Remove(entity);
        }

     
        public async Task Save()
        {
             await _context.SaveChangesAsync();
        }

        
    }
}
