namespace ProductsWithRouting.Models
{
    public class ProductError
    {
        public int ProductId { get; set; }
        public string Message {  get; set; } = string.Empty;

        public ProductError(int productId, string message)
        {
            ProductId = productId;
            Message = message;
        }

        public ProductError() { }
    }
}
