using Microsoft.EntityFrameworkCore;

namespace SupleStore.Models
{
    public class Context: DbContext
    {
        public Context(DbContextOptions<Context> options): base(options) 
        {
        }
        public DbSet<Productos> Productos { get; set; }
        public DbSet<Categorias> Categorias { get; set; }
    }
    
}
