using System.Text;
using System.Text.RegularExpressions;
using codep.BL;
using codep.ConsolePL;
using codep.DAL;
using codep.Model;
internal class Program
{
    private static void Main(string[] args)
    {
        Console.InputEncoding = Encoding.Unicode;
        Console.OutputEncoding = Encoding.Unicode;

        StaffBL staffBl = new StaffBL();
        OrderBL orderBl = new OrderBL();
        ProductsBL productsBl = new ProductsBL();
        AdminBL adminBl = new AdminBL();
        OrderDAL orderDal = new OrderDAL();
        HT1 ht1 = new HT1();

        MainMenu();


        void WaitForButton(string msg)
        {
            Console.Write(msg);
            Console.ReadKey();
        }

        static bool IsContinue(string text)
        {
            string Continue;
            bool isMatch;
            Console.Write(text);
            Continue = Console.ReadLine() ?? "";
            isMatch = Regex.IsMatch(Continue, @"^[yYnN]$");
            while (!isMatch)
            {
                Console.Write(" Chọn (Y/N)!!!: ");
                Continue = Console.ReadLine() ?? "";
                isMatch = Regex.IsMatch(Continue, @"^[yYnN]$");
            }
            if (Continue == "y" || Continue == "Y") return true;
            return false;
        }

        string GetPassword()
        {
            StringBuilder sb = new StringBuilder();
            while (true)
            {
                ConsoleKeyInfo cki = Console.ReadKey(true);
                if (cki.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    break;
                }
                if (cki.Key == ConsoleKey.Backspace)
                {
                    if (sb.Length > 0)
                    {
                        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                        Console.Write(" ");
                        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                        sb.Length--;
                    }
                    continue;
                }
                Console.Write('*');

                sb.Append(cki.KeyChar);
            }
            return sb.ToString();
        }

        int Menu(string[] menu, string name)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            HT1.Line(68);
            Console.WriteLine($"{name}");
            HT1.Line(68);
            Console.ResetColor();
            for (int i = 0; i < menu.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {menu[i]}");
            }
            HT1.Line(68);
            int choice;
            do
            {
                Console.Write("Chọn: ");
                int.TryParse(Console.ReadLine(), out choice);
            } while (choice < 1 || choice > menu.Length);
            return choice;
        }

// Menu main

        void MainMenu()
        {
            Console.Clear();
            string[] menu = { "Đăng nhập", "Đăng ký", "Admin", "Thoát" };
            string name = @"
            
                    ░█▄█░░░█▀▀░░░█░█░░░█▀█░░░█▀█
                    ░█░█░░░▀▀█░░░█▀█░░░█░█░░░█▀▀
                    ░▀░▀░░░▀▀▀░░░▀░▀░░░▀▀▀░░░▀░░
                    
                    ";
            int choice;
            Staff staff = new Staff();
            AdminS admin = new AdminS();
            while (true)
            {
                choice = Menu(menu, name);
                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        staffBl.LoginUser();
                        MenuStore(staff);
                    break;
                    case 2:
                        Console.Clear();
                        staffBl.RegU(staff);
                        MainMenu();
                    break;
                    case 3:
                        Console.Clear();
                        adminBl.LoginAdmin(admin);
                        AdminS();
                    break;
                    case 4:
                        Console.Clear();
                        if (IsContinue("Bạn có chắc là muốn thoát? (Y/N): "))
                        {
                            Console.WriteLine("Đã thoát ứng dụng!");
                            Environment.Exit(0);
                        }
                    break;
                }
            }
        }

// Menu cho Admin

        void MenuAdmin(AdminS admin) 
        {
            Console.Clear();
            string[] menuA = { "Chức năng dành cho ADMIN ", "Xem thanh toán", "Đăng xuất" };
            string nameM =@"                      ___  _____ ___  __ __ __  __ 
                     ||=|| ||  ) || \/ | || ||\\|| 
                     || || ||_// ||    | || || \|| 
                     ";
            int ch;
            do
            {
                ch = Menu(menuA, nameM);
                switch (ch)
                {
                    case 1:
                        Console.Clear();
                        MenuUp(admin);
                        break;
                    case 2:
                        Console.Clear();
                        orderBl.ShowAllB();
                        WaitForButton("Nhập phím bất kỳ để tiếp tục...");
                        break;
                    case 3:
                        Console.Clear();
                        if (IsContinue("Bạn có muốn đăng xuất? (Y/N): "))
                        {
                            Console.WriteLine("Đăng xuất thành công!");
                            WaitForButton("Nhập phím bất kỳ để tiếp tục...");
                        }
                        else
                        {
                            MenuAdmin(admin);
                        }
                        break;
                }
            } while (ch != menuA.Length);
        }
// Menu cập nhật cho admin

        void MenuUp(AdminS admin)
        {
            Console.Clear();
            string[] menu = { "Thêm sản phẩm", "Xem tất cả sản phẩm", "Xóa sản phẩm", "Sửa sản phẩm", "Thoát " };
            HT1.Line(200);
            string name =@"
            
            ░█▀▀░█▀▄░▀█▀░▀█▀░░░█▀█░█▀▄░█▀█░█▀▄░█░█░█▀▀░▀█▀
            ░█▀▀░█░█░░█░░░█░░░░█▀▀░█▀▄░█░█░█░█░█░█░█░░░░█░
            ░▀▀▀░▀▀░░▀▀▀░░▀░░░░▀░░░▀░▀░▀▀▀░▀▀░░▀▀▀░▀▀▀░░▀░
                                    ";
            HT1.Line(200);
            int ch;
            Product product = new Product();
            do
            {
                ch = Menu(menu, name);
                switch (ch)
                {
                    case 1:
                        Console.Clear();
                        productsBl.InPro(product);
                        WaitForButton("Nhập phím bất kỳ để tiếp tục...");
                        break;
                    case 2:
                        Console.Clear();
                        string commandTextGetAllProduct = "select product.product_id, product.product_name, product.product_price, product.product_description, product.product_quantity, category.category_name from product inner join category on product.category_id = category.category_id;";
                        productsBl.GetAllProduct(commandTextGetAllProduct);
                        WaitForButton("Nhập phím bất kỳ để tiếp tục...");
                        break;
                    case 3:
                        Console.Clear();
                        productsBl.deletePro(product);
                        WaitForButton("Nhập phím bất kỳ để tiếp tục...");
                        break;
                    case 4:
                        Console.Clear();
                        productsBl.UpdatePro(product);
                        WaitForButton("Nhập phím bất kỳ để tiếp tục...");
                        break;
                    case 5:
                        Console.Clear();
                        MenuAdmin(admin);
                        break;
                    default:
                        break;
                }

            } while (ch != menu.Length);
        }

// Menu đăng nhập cho admin

        void AdminS()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("===============================================================");
            Console.WriteLine(@"                  ___  _____ ___  __ __ __  __ 
                 ||=|| ||  ) || \/ | || ||\\|| 
                 || || ||_// ||    | || || \||         ");
            Console.WriteLine();
            Console.WriteLine("===============================================================");
            Console.ResetColor();
            Console.Write("Tên đăng nhập: ");
            string user = Console.ReadLine() ?? "";
            Console.Write("Mật khẩu     : ");
            string passA = GetPassword();
            AdminS admin = new AdminS() { admin_User = user, admin_pass = passA };
            admin = adminBl.LoginAdmin(admin);
            if (admin.adminId == 1 || admin.adminId == 2)
            {
                MenuAdmin(admin);
            }
            else
            {
                Console.WriteLine("Đăng nhập thất bại, vui lòng thử lại!");
                WaitForButton("Nhập phím bất kỳ để tiếp tục...");
            }
        }

// menu cho user

        void MenuStore(Staff staff)
        {
            Console.Clear();
            string[] menu = { "Tìm product ", "Tạo đơn hàng mới", "Đăng xuất" };
            HT1.Line(250);
            string name =@"
            
░█░█░█░█░█▀█░▀█▀░░░█▀▄░█▀█░░░█░█░█▀█░█░█░█▀▄░░░█░█░█▀█░█▀█░▀█▀░░░▀▀█
░█▄█░█▀█░█▀█░░█░░░░█░█░█░█░░░░█░░█░█░█░█░█▀▄░░░█▄█░█▀█░█░█░░█░░░░░▀░
░▀░▀░▀░▀░▀░▀░░▀░░░░▀▀░░▀▀▀░░░░▀░░▀▀▀░▀▀▀░▀░▀░░░▀░▀░▀░▀░▀░▀░░▀░░░░░▀░

            ";
            HT1.Line(250);
            int choice;
            do
            {
                choice = Menu(menu, name);
                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        MenuSearchProduct();
                        break;

                    case 2:
                        Console.Clear();
                        orderBl.CreateNewOrder(staff);
                        break;
                    case 3:
                        Console.Clear();
                        if (IsContinue("Bạn có muốn đăng xuất? (Y/N): "))
                        {
                            Console.WriteLine("Đăng xuất thành công!");
                            WaitForButton("Nhập phím bất kỳ để tiếp tục...");
                        }
                        else
                        {   
                            Console.Clear();
                            MenuStore(staff);
                        }
                        break;
                }
            } while (choice != menu.Length);
        }
// tìm sản phẩm cho user

        void MenuSearchProduct()
        {
            Console.Clear();
            string[] menu = { "Xem tất cả loại product", "Tìm kiếm product theo Id", "Quay lại" };
            Console.ForegroundColor = ConsoleColor.Green;
            string name = "Sản phẩm tìm kiếm!!! ";
            int choice;
            do
            {
                choice = Menu(menu, $"{name,44}");
                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        string commandTextGetAllProduct = "select product.product_id, product.product_name, product.product_price, product.product_description, product.product_quantity, category.category_name from product inner join category on product.category_id = category.category_id;";
                        productsBl.GetAllProduct(commandTextGetAllProduct);
                        WaitForButton("Nhập phím bất kỳ để tiếp tục...");
                        break;
                    case 2:
                        Console.Clear();
                        Console.Write("Nhập id để tìm kiếm: ");
                        string id = Console.ReadLine() ?? "";
                        productsBl.SearchProductByID(id);
                        WaitForButton("Nhập phím bất kỳ để tiếp tục...");
                        break;
                    default:
                        break;
                }
            } while (choice != menu.Length);
        }
    }
}