
namespace codep.Model
{
    public class Orders
    {
        public int orderId { get; set; }

        public Customer? orderCustomer { get; set; }
        public Staff? orderStaff { get; set; }
        public DateTime orderDate { get; set; }
        public List<Product> productsList { get; set; }
        public decimal total { get; set; }
        public Orders()
        {
            this.productsList = new List<Product>();
        }
    }
    public class Payment
    {
        public decimal paymentAmount { get; set; }
        public decimal refund { get; set; }
    }
}