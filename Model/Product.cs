namespace codep.Model
{
    public class Product
    {
        public int productId { get; set; }
        public string? productCategory { get; set; }
        public Category? productCate { get; set; }

        public string? productName { get; set; }
        public decimal productPrice { get; set; }
        public string? productDescription { get; set; }
        public int productQuantity { get; set; }
        public decimal productAmount { get; set; }
    }

}