namespace ShopMVC.Models.DTOs
{
    public class ItemDisplayModel
    {
        public IEnumerable<Item> Items { get; set; }

        public IEnumerable<TypeItem> TypeItems { get; set; }

        public string STerm { get; set; } = "";

        public int TypeId { get; set; } = 0;
    }
}
