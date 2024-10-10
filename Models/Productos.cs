using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupleStore.Models
{
    public class Productos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int precio { get; set; }
        public int CategoryId { get; set; }
        

        [ForeignKey("CategoryId")]
        public virtual Categorias Categorias { get; set; }

    }
}
