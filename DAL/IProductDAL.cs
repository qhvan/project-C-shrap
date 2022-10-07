using codep.Model;

namespace codep.DAL
{
    public interface IProductDAL
    {
        public Product GetProductById(string searchKeyWord, Product product);
        public List<Product> GetProductList(List<Product> list, string commandText);
        public int Update(Product product);
        public int Delete(Product product);
        public int InS(Product product);
    }
}