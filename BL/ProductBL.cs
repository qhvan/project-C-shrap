using codep.Model;
using System.Text.RegularExpressions;
using codep.DAL;
using codep.ConsolePL;
namespace codep.BL
{
    public class ProductsBL
    {   
        private ProductsDAL productDal = new ProductsDAL();
        public Product SearchProductByID(string searchKeyWord)
        {
            Product product = new Product();
            product = productDal.GetProductById(searchKeyWord, product);
            string search = '"' + searchKeyWord + '"';
            if (product.productId <= 0)
            {
                Console.WriteLine($"Không tồn tại sản phẩm phù hợp với từ khoá là {search}");
            }
            else
            {
                ShowProductDatail(product, searchKeyWord);
            }
            return product;
        }
// IN TẤT CẢ DANH SÁCH BÁNH CHO USER
        public void GetAllProduct(string commandText)
        {
            List<Product> list = new List<Product>();
            list = productDal.GetProductList(list, commandText);
            if (list.Count == 0)
            {
                Console.WriteLine("Không có sản phẩm!");
                WaitForButton("Nhập phím bất kỳ để tiếp tục...");
            }
            else
            {
                int size = 10;
                int page = 1;
                int pages = (int)Math.Ceiling((double)list.Count / size);
                int i, k = 0;
                string choice, price;
                for (; ; )
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("=====================================================================================================================================================");
                    Console.WriteLine("|                                                              Tất cả các sản phẩm!!!                                                               |");
                    Console.WriteLine($"|                                                                                                                              Trang {page}/{pages}            |");
                    Console.WriteLine("=====================================================================================================================================================");
                    Console.WriteLine("| Mã      | Tên                                                                 | Giá           | Loại           | Số lượng   | Mô tả               |");
                    if (list.Count < size)
                    {
                        for (i = 0; i < list.Count; i++)
                        {
                            price = FormatCurrency(list[i].productPrice.ToString());
                            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------------------------");
                            Console.WriteLine($"| {list[i].productId,-7} | {list[i].productName,-67} | {price,-13} | {list[i].productCategory,-14} | {list[i].productQuantity,-10} | {list[i].productDescription,-20}|");
                        }
                    }
                    else
                    {
                        for (i = ((page - 1)) * size; i < k + 10; i++)
                        {
                            if (i == list.Count) break;
                            price = FormatCurrency(list[i].productPrice.ToString());
                            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------------------------");
                            Console.WriteLine($"| {list[i].productId,-7} | {list[i].productName,-67} | {price,-13} | {list[i].productCategory,-14} | {list[i].productQuantity,-10} | {list[i].productDescription,-20}|");
                        }
                    }
                    Console.WriteLine("=====================================================================================================================================================");
                    Console.WriteLine("Nhập P để xem trang trước.");
                    Console.WriteLine("Nhập N để xem trang sau.");
                    Console.WriteLine("Nhập 0 để quay lại.");
                            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------------------------");
                    Console.Write("Chọn: ");
                    choice = Console.ReadLine() ?? "";
                    while (!(Regex.IsMatch(choice, @"([PpNn]|[1-9]|^0$)")))
                    {
                        Console.Write("Lựa chọn không hợp lệ! Chọn lại: ");
                        choice = Console.ReadLine() ?? "";
                    }
                    choice = choice.Trim();
                    choice = choice.ToLower();
                    string number = Regex.Match(choice, @"\d+").Value;
                    string pageNum = "p" + number;
                    if (choice == "n")
                    {
                        if (page == pages)
                        {
                            Console.Clear();
                            WaitForButton("Không có trang sau! Nhập phím bất kỳ để tiếp tục...");
                        }
                        else
                        {
                            page = page + 1;
                            k = k + 10;
                        }
                    }
                    else if (choice == "p")
                    {
                        if (page == 1)
                        {
                            Console.Clear();
                            WaitForButton("Không có trang trước! Nhập phím bất kỳ để tiếp tục...");
                        }
                        else
                        {
                            page = page - 1;
                            k = k - 10;
                        }
                    }
                    else if (choice == pageNum)
                    {
                        if (int.Parse(number) < 0 || int.Parse(number) > pages || int.Parse(number) == 0)
                        {
                            Console.WriteLine($"Không tồn tại trang {int.Parse(number)}");
                            WaitForButton("Nhập phím bất kỳ để tiếp tục...");
                        }
                        else
                        {
                            page = int.Parse(number);
                            k = (int.Parse(number) - 1) * 10;
                        }
                    }
                    else if (choice == "0") return;
                    else
                    {
                        bool found = false;
                        string search1 = '"' + choice + '"';
                        for (i = ((page - 1)) * size; i < k + 10; i++)
                        {
                            try
                            {
                                if (int.Parse(choice) == list[i].productId)
                                {       
                                    Console.Clear();
                                    ShowProductDatail(list[i], search1);
                                    WaitForButton("Nhập phím bất kỳ để tiếp tục...");
                                    found = true;
                                    break;
                                }
                            }
                            catch (System.FormatException) { }
                            catch (System.ArgumentOutOfRangeException) { }
                        }
                        if (!(found))
                        {
                            Console.WriteLine("ID không phù hợp!");
                            WaitForButton("Nhập phím bất kỳ để tiếp tục...");
                        }
                    }
                }
            }
        }
      
 // IN THÔNG TIN CHI TIẾT
        public void ShowProductDatail(Product product, string search)
        {
            Console.Clear();
            string price = FormatCurrency(product.productPrice.ToString());
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("================================================================");
            Console.WriteLine($"             Thông tin chi tiết sản phẩm có ID là {search,-30} ");
            Console.WriteLine("================================================================");
            Console.WriteLine($"| Mã :               | {product.productId,-39} |");
            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine($"| Tên :              | {product.productName,-39} | ");
            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine($"| Giá:               | {product.productPrice,-39} |");
            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine($"| Phân loại:         | {product.productCategory,-39} | ");
            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine($"| Số lượng:          | {product.productQuantity,-39} | ");
            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine($"| Mô tả:             | {product.productDescription,-39} |");
            Console.WriteLine("================================================================");
        }
        public string FormatCurrency(string currency)
        {
            for (int k = currency.Length - 3; k > 0; k = k - 3)
            {
                currency = currency.Insert(k, ".");
            }
            currency = currency + " VND";
            return currency;
        }
        void WaitForButton(string msg)
        {
            Console.Write(msg);
            Console.ReadKey();
        }
        public int UpdatePro(Product product)
        {
            Console.Clear();
            int rs;
            Console.ForegroundColor = ConsoleColor.Green;
            HT1.Line(70);
            Console.WriteLine(@"U   U     PPPP      DDD       AA      TTTTTT     EEEE 
U   U     P   P     D  D     A  A       TT       E    
U   U     PPPP      D  D     AAAA       TT       EEE  
U   U     P         D  D     A  A       TT       E    
 UUU      P         DDD      A  A       TT       EEEE ");
            HT1.Line(70);
            Console.ResetColor();
            Console.Write("Nhập Id sản phẩm      : ");
            product.productId = Convert.ToInt32(Console.ReadLine());
            Console.Write("Nhập tên sản phẩm     : ");
            product.productName = Console.ReadLine();
            Console.Write("Nhập danh mục sản phẩm: ");
            product.productCategory = Console.ReadLine();
            Console.Write("Nhập mô tả sản phẩm   : ");
            product.productDescription = Console.ReadLine();
            Console.Write("Nhập giá sản phẩm     : ");
            product.productPrice = Convert.ToDecimal(Console.ReadLine());
            Console.Write("Nhập số lượng sản phẩm: ");
            product.productAmount = Convert.ToDecimal(Console.ReadLine());
            rs = productDal.Update(product);
            if (rs == 1)
            {

                Console.WriteLine("Update thành công");
                WaitForButton("Nhập phím bất kỳ để tiếp tục...");
            }
            else
            {
                Console.WriteLine("Không tìm thấy ID sản phẩm");
            }
            return rs;
        }

        public int deletePro(Product product)
        {
            Console.Clear();
            int rs;
            Console.ForegroundColor = ConsoleColor.Green;
            HT1.Line(50);
            Console.WriteLine(@"DDD      EEEE     L        EEEE     TTTTTT     EEEE 
D  D     E        L        E          TT       E    
D  D     EEE      L        EEE        TT       EEE  
D  D     E        L        E          TT       E    
DDD      EEEE     LLLL     EEEE       TT       EEEE ");
            HT1.Line(50);
            Console.ResetColor();
            Console.Write("Nhập Id sản phẩm: ");
            product.productId = Convert.ToInt32(Console.ReadLine());
            rs = productDal.Delete(product);
            if (rs == 1)
            {

                Console.WriteLine($"Đã xóa sản phẩm với ID là {product.productId}");
                WaitForButton("Nhập phím bất kỳ để tiếp tục...");
            }
            else
            {
                Console.WriteLine($"Không tìm thấy ID sản phẩm la {product.productId} ");
                WaitForButton("Nhập phím bất kỳ để tiếp tục...");

            }
            return rs;

        }

        public int InPro(Product product)
        {
            int result;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            HT1.Line(70);
            Console.WriteLine(@"III     N   N      SSS      EEEE     RRRR      TTTTTT     
 I      NN  N     S         E        R   R       TT       
 I      N N N      SSS      EEE      RRRR        TT       
 I      N  NN         S     E        R R         TT       
III     N   N     SSSS      EEEE     R  RR       TT       ");
            HT1.Line(70);
            Console.ResetColor();
            Console.Write("Mã danh mục sản phẩm: ");
            product.productCategory = Console.ReadLine();
            Console.Write("Tên sản phẩm        : ");
            product.productName = Console.ReadLine();
            Console.Write("Giá sản phẩm        : ");
            product.productPrice = Convert.ToDecimal(Console.ReadLine());
            Console.Write("Mô tả sản phẩm      : ");
            product.productDescription = Console.ReadLine();
            Console.Write("Số lượng sản phẩm   : ");
            product.productAmount = Convert.ToInt32(Console.ReadLine());
            result = productDal.InS(product);
            if (result == 1)
            {
                Console.WriteLine("Thêm thành công!!!");
                WaitForButton("Nhập phím bất kỳ để tiếp tục...");
            }
            else
            {
                Console.WriteLine("Thêm không thành công!! Đã tồn tại sản phẩm");
                WaitForButton("Nhập phím bất kỳ để tiếp tục...");
            }
            return result;
        }
    }
}