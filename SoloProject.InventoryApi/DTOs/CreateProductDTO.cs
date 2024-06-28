namespace SoloProject.InventoryApi.DTOs
{
    public class CreateProductDTO
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
    }
}
