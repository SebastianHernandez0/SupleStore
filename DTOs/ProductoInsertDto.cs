namespace SupleStore.DTOs
{
    public class ProductoInsertDto
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public int precio { get; set; }
        public int CategoryId { get; set; }
    }
}
