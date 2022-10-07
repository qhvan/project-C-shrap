using codep.Model;
using MySql.Data.MySqlClient;

namespace codep.DAL
{
    public class ProductsDAL:IProductDAL
    {
        private MySqlConnection connection = DbConfig.GetConnection();
        private Product GetProduct(MySqlDataReader reader)
        {
            Product product = new Product();
            product.productCate = new Category();
            product.productId = reader.GetInt32("product_id");
            product.productName = reader.GetString("product_name");
            product.productPrice = reader.GetDecimal("product_price");
            product.productDescription = reader.GetString("product_description");
            product.productQuantity = reader.GetInt32("product_quantity");
            product.productCate.categoryName = reader.GetString("category_name");
            return product;
        }
        public Product GetProductById(string searchKeyWord, Product product)
        {
            try
            {
                connection.Open();
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = $"select product.product_id, product.product_name, product.product_price, product.product_description, product.product_quantity, category.category_name from product inner join category on product.category_id = category.category_id where product.product_id = '{searchKeyWord}';";
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    product.productId = reader.GetInt32("product_id");
                    product.productName = reader.GetString("product_name");
                    product.productPrice = reader.GetDecimal("product_price");
                    product.productDescription = reader.GetString("product_description");
                    product.productQuantity = reader.GetInt32("product_quantity");
                    product.productCategory = reader.GetString("category_name");
                }
                else
                {
                    product.productId = -1;
                }
                reader.Close();
            }
            catch
            {
                product.productId = -1;
            }
            finally
            {
                connection.Close();
            }
            return product;
        }
        public List<Product> GetProductList(List<Product> list, string commandText)
        {
            lock (connection)
            {
                try
                {
                    connection.Open();
                    MySqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = commandText;
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Product product = new Product();
                        product.productId = reader.GetInt32("product_id");
                        product.productName = reader.GetString("product_name");
                        product.productPrice = reader.GetDecimal("product_price");
                        product.productDescription = reader.GetString("product_description");
                        product.productQuantity = reader.GetInt32("product_quantity");
                        product.productCategory = reader.GetString("category_name");
                        list.Add(product);
                    }
                    reader.Close();
                }
                catch
                {
                    Console.WriteLine("Disconnected database");
                }
                finally
                {
                    connection.Close();
                }
                return list;
            }
        }
        public int Update(Product product)
        {
            MySqlCommand cmd = new MySqlCommand("updatePro", connection);
            int rs = 1;
            connection.Open();
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@proId", product.productId);
                cmd.Parameters.AddWithValue("@proName", product.productName);
                cmd.Parameters.AddWithValue("@proCt", product.productCategory);
                cmd.Parameters.AddWithValue("@proD", product.productDescription);
                cmd.Parameters.AddWithValue("@proPrice", product.productPrice);
                cmd.Parameters.AddWithValue("@proQty", product.productAmount);
                cmd.ExecuteNonQuery();
            }
            catch
            {
                rs = 0;
            }
            finally
            {
                DbConfig.CloseConnection();
            }
            return rs;
        }

        public int Delete(Product product)
        {
            int rs = 1;
            connection.Open();
            MySqlCommand cmd = new MySqlCommand("deletePr", connection);
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", product.productId);
                cmd.ExecuteNonQuery();
            }
            catch
            {
                rs = 0;
            }
            finally
            {
                DbConfig.CloseConnection();
            }
            return rs;
        }

        public int InS(Product product)
        {
            int rs = 1;
            connection.Open();
            MySqlCommand cmd = new MySqlCommand("In_Product", connection);
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@category_idNew", product.productCategory);
                cmd.Parameters.AddWithValue("@product_nameNew", product.productName);
                cmd.Parameters.AddWithValue("@product_desNew", product.productDescription);
                cmd.Parameters.AddWithValue("@product_priceNew", product.productPrice);
                cmd.Parameters.AddWithValue("@product_quanNew", product.productAmount);
                cmd.ExecuteNonQuery();
            }
            catch
            {
                rs = 0;
            }
            finally
            {
                DbConfig.CloseConnection();
            }
            return rs;
        }
    }
}