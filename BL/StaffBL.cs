using codep.DAL;
using codep.Model;
using System.Text;
using codep.ConsolePL;
using System.Text.RegularExpressions;

namespace codep.BL
{
    public class StaffBL
    {
        private StaffDAL staffDal = new StaffDAL();

// đăng nhập user

        public Staff LoginUser()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            HT1.Line(40);
            Console.WriteLine(@"L                                    
L                        ii          
L        ooo     ggg            nnn  
L        o o     g g     ii     n  n 
LLLL     ooo     ggg     ii     n  n 
                   g                 
                 ggg     ");
            HT1.Line(40);
            Console.ResetColor();
            Console.Write("Tên đăng nhập: ");
            string userName = Console.ReadLine() ?? "";
            Console.Write("Mật khẩu     : ");
            string password = GetPassword();
            Staff staff = new Staff() { userName = userName, password = password };
            staff = staffDal.Login(staff);
            if (staff.userName == null)
            {
                Console.WriteLine("Xin Chào!!!");
                WaitForButton("Nhập phím bất kỳ để tiếp tục...");
            }
            else
            {
                Console.WriteLine("Tạm Biệt!!!");
                if (IsContinue("Bạn có chắc là muốn đăng kí hay không? (Y/N): "))
                {
                    RegU(staff);
                }
                else
                {
                    return LoginUser();
                }
            }

            return staffDal.Login(staff);
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


        // đăng kí user
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
        void WaitForButton(string msg)
        {
            Console.Write(msg);
            Console.ReadKey();
        }

        public int RegU(Staff staff)
        {
            Console.Clear();
            int rs;
            Console.ForegroundColor = ConsoleColor.Yellow;
            HT1.Line(60);
            Console.WriteLine(@"RRRR                                      t                  
R   R                     ii              t                  
RRRR      eee     ggg             ss     ttt     eee     rrr 
R R       e e     g g     ii      s       t      e e     r   
R  RR     ee      ggg     ii     ss       tt     ee      r   
                    g                                        
                  ggg         ");
            HT1.Line(60);
            Console.ResetColor();
            Console.Write("Tên của bạn: ");
            //staff.staffName = GetName();
            staff.staffName = Console.ReadLine();
            staff.staffPhone = GetPhoneNumber();
            Console.Write("Thành Phố: ");
            staff.staffAddress = Console.ReadLine();
            staff.userName = GetNameAccount();
            staff.password = GetPass();
            rs = staffDal.RegUser(staff);
            if (rs == 1)
            {
                Console.WriteLine("Tạo tài khoản thành công!!");
                WaitForButton("Nhập phím bất kỳ để tiếp tục...");

            }
            else
            {
                Console.WriteLine("Tạo tài không khoản thành công!!! Đã tồn tại SĐT");
                WaitForButton("Nhập phím bất kỳ để tiếp tục...");
            }
            return rs;
        }

        string GetNameAccount()
        {
            bool validate = false;
            string output = "";
            string pattern = @"^([^<>\s\W]){5,30}$";
            while (validate == false)
            {
                Console.Write("Nhập tài khoản: ");
                output = Console.ReadLine() ?? "";
                validate = Regex.IsMatch(output, pattern);

                if (validate == false)
                {
                    Console.WriteLine("không nhập khoảng trắng hoặc không đủ kí tự!!");
                    Console.ReadKey();
                }
                else
                {
                }
            }
            return output;
        }
        string GetPass()
        {
            bool validate = false;
            string output = "";
            string pattern = @"^([^<>\s\W]){5,30}$";
            while (validate == false)
            {
                Console.Write("Mật Khẩu: ");
                output = GetPassword();
                validate = Regex.IsMatch(output, pattern);

                if (validate == false)
                {
                    Console.WriteLine("không nhập khoảng trắng hoặc không đủ kí tự!!");
                }
                else
                {
                }
            }
            return output;
        }
        string GetPhoneNumber()
        {
            bool validate = false;
            string output = "";
            string pattern = @"^([^\s\D]{6})$";
            while (validate == false)
            {
                Console.Write("Sđt: ");
                output = Console.ReadLine() ?? "";
                validate = Regex.IsMatch(output, pattern);

                if (validate == false)
                {
                    Console.WriteLine("không nhập khoảng trắng hoặc không đủ 6 số!!");
                }
                else
                {
                }
            }
            return output;
        }
        public string GetMoney()
        {
            bool validate = false;
            string output = "";
            string pattern = @"^([^\s\D]{4,9})$";
            while (validate == false)
            {
                output = Console.ReadLine() ?? "";
                validate = Regex.IsMatch(output, pattern);

                if (validate == false)
                {
                    Console.WriteLine("không nhập khoảng trắng hoặc không đủ 6 số!!");
                }
                else
                {
                }
            }
            return output;
        }
    }
}