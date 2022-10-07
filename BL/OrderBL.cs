using codep.Model;
using codep.DAL;
using codep.ConsolePL;
using System.Text.RegularExpressions;

namespace codep.BL
{
    public class OrderBL
    {
        private OrderDAL orderDal = new OrderDAL();

        public bool CreateOrder(Orders order)
        {
            return orderDal.CreateOrder(order);
        }
        public void ShowAllB()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            OrderDAL orderDAL = new OrderDAL();
            List<Orders> ordersList = new List<Orders>();
            ordersList = orderDAL.GetOr();
            if (ordersList == null || ordersList.Count == 0)
            {
                Console.WriteLine("Không có order nào cả!!");
                return;
            }
            else
            {
                int size = 10;
                int page = 1;
                int pages = (int)Math.Ceiling((double)ordersList.Count / size);
                int k = 0;
                string choose;
                while (true)
                {
                    {
                        HT1.Line(90);
                        Console.Clear();
                        Console.WriteLine(@"
                        
                ░█▀█░█▀█░█░█░█▄█░█▀▀░█▀█░▀█▀░░░█░█░▀█▀░█▀▀░▀█▀░█▀█░█▀▄░█░█
                ░█▀▀░█▀█░░█░░█░█░█▀▀░█░█░░█░░░░█▀█░░█░░▀▀█░░█░░█░█░█▀▄░░█░
                ░▀░░░▀░▀░░▀░░▀░▀░▀▀▀░▀░▀░░▀░░░░▀░▀░▀▀▀░▀▀▀░░▀░░▀▀▀░▀░▀░░▀░
                        
                        ");
                        Console.WriteLine($"                                                                       Page {page}/{pages}      ");
                        HT1.Line(90);
                        Console.WriteLine("| Order id         Thời gian tạo                  Tổng tiền             Trạng thái       |");
                        Console.WriteLine("| --------         ----------------------         -------------         ---------------  |");
                        if (ordersList.Count < size)
                        {
                            for (int i = 0; i < ordersList.Count; i++)
                            {
                                Console.WriteLine($"| {ordersList[i].orderId,-17}{ordersList[i].orderDate,-30} {ordersList[i].total.ToString(),-21} {"Đã thanh toán",-12}    |");
                            }
                        }
                        else
                        {
                            for (int i = ((page - 1)) * size; i < k + 10; i++)
                            {
                                if (i == ordersList.Count) break;
                                Console.WriteLine($"| {ordersList[i].orderId,-17}{ordersList[i].orderDate,-30} {ordersList[i].total.ToString(),-21} {"Đã thanh toán",-12}    |");
                            }
                        }
                    }
                    HT1.Line(90);
                    Console.WriteLine("Nhập P xem trang trước!!");
                    Console.WriteLine("Nhập N xem trang tiếp!!");
                    Console.WriteLine("Nhập 0 để trở về!!");
                    HT1.Line(90);
                    Console.Write("Choose: ");
                    choose = Console.ReadLine() ?? "";
                    while (true)
                    {
                        if (Regex.Match(choose, @"([PpNn]|[1-9]|^0$)").Success)
                        {
                            break;
                        }
                        else
                        {
                            Console.Write("Invalid selection! re-select: ");
                            choose = Console.ReadLine() ?? "";
                        }
                    }
                    choose = choose.Trim();
                    choose = choose.ToLower();
                    string number = Regex.Match(choose, @"\d+").Value;
                    if (choose == "n")
                    {
                        if (page == pages)
                        {   
                            Console.Clear();
                            WaitForButton("No next page! Enter any key to continue...");
                        }
                        else
                        {
                            page = page + 1;
                            k = k + 10;
                        }
                    }
                    else if (choose == "p")
                    {
                        if (page == 1)
                        {   
                            Console.Clear();
                            WaitForButton("No previous page! Enter any key to continue...");
                        }
                        else
                        {
                            page = page - 1;
                            k = k - 10;
                        }
                    }
                    else if (choose == "0")
                    {
                        return;

                    }
                }
            }
        }
        public static void WaitForButton(string msg)
        {
            Console.Write(msg);
            Console.ReadKey();
        }
        public void CreateNewOrder(Staff staff)
        {
            ProductsBL productsBl = new ProductsBL();
            StaffBL staffBl = new StaffBL();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("===============================================================");
            Console.WriteLine("|                         Tạo hoá đơn                         |");
            Console.WriteLine("===============================================================");
            Console.ResetColor();
            Orders order = new Orders();
            order.orderStaff = staff;
            do
            {   
                Console.Write("Nhập ID của product để thêm vào hoá đơn:");
                string id = Console.ReadLine() ?? "";
                Product product = productsBl.SearchProductByID(id);
                if (product.productId == 0)
                {
                    continue;
                }
                else
                {
                    string strQuantity;
                    bool isSuccess;
                    int quantity;
                    Console.Write("Nhập số lượng sách cần mua: ");
                    strQuantity = Console.ReadLine() ?? "";
                    isSuccess = int.TryParse(strQuantity, out quantity);
                    while (!isSuccess)
                    {
                        Console.Write("Số lượng không hợp lệ! Nhập số lượng: ");
                        strQuantity = Console.ReadLine() ?? "";
                        isSuccess = int.TryParse(strQuantity, out quantity);
                    }
                    if (quantity <= 0)
                    {
                        Console.WriteLine("Thêm không thành công");
                        Console.WriteLine("Quyển sách này đã hết hàng!");
                        continue;
                    }
                    if (quantity <= 0)
                    {
                        Console.WriteLine("Thêm không thành công");
                        Console.WriteLine("Số lượng nhập vào không hợp lệ!");
                        continue;
                    }
                    if (quantity > product.productQuantity)
                    {
                        Console.WriteLine("Thêm không thành công");
                        Console.WriteLine("Số lượng mua vượt quá số lượng sách có sẵn!");
                        continue;
                    }
                    decimal amount = quantity * product.productPrice;
                    product.productQuantity = quantity;
                    product.productAmount = amount;
                    bool add = true;
                    if (order.productsList == null || order.productsList.Count == 0)
                    {
                        order.productsList!.Add(product);
                    }
                    else
                    {
                        for (int n = 0; n < order.productsList.Count; n++)
                        {
                            if (int.Parse(id) == order.productsList[n].productId)
                            {
                                order.productsList[n].productQuantity += quantity;
                                order.productsList[n].productAmount += amount;
                                add = false;
                            }
                        }
                        if (add) order.productsList.Add(product);
                    }
                }
            } while (IsContinue("Bạn có muốn thêm sản phẩm khác vào hoá đơn không? (Y/N): "));

            if (order.productsList == null || order.productsList.Count == 0)
                Console.WriteLine("Hoá đơn chưa có sản phẩm!");

            if (CreateOrder(order))
            {

                Console.WriteLine("Tạo hoá đơn thành công!");
                WaitForButton("Nhập phím bất kỳ để xem hoá đơn...");
                Console.Clear();
                string price, amount;
                Console.ForegroundColor = ConsoleColor.Red;
                HT1.Line(97);
                Console.WriteLine("|                                       Hoá đơn bán hàng                                        |");
                HT1.Line(97);
                Console.ResetColor();
                Console.WriteLine($"| Thời gian: {order.orderDate,-61}   Mã hoá đơn: {order.orderId,6} |");
                Console.WriteLine($"|                                                                              Địa chỉ: Hạ Long |");
                Console.WriteLine("| Mặt hàng                                                            Đơn giá    SL      T.Tiền |");
                HT1.Line(97);
                foreach (Product product in order.productsList!)
                {
                    price = FormatCurrency(product.productPrice.ToString());
                    amount = FormatCurrency(product.productAmount.ToString());
                    Console.WriteLine($"| {product.productName,-65} {price,9} {product.productQuantity,5} {amount,11} |");
                    order.total += product.productAmount;
                }
                string total = FormatCurrency(order.total.ToString());
                Console.ForegroundColor = ConsoleColor.Yellow;
                HT1.Line(97);
                Console.WriteLine($"| TỔNG TIỀN PHẢI THANH TOÁN {total,63} VND |");
                HT1.Line(97);
                Console.WriteLine($"| Tên khách hàng: {order.orderCustomer!.customer_Name,-49}                             |");
                HT1.Line(97);
                Payment payment = new Payment();
                string paymentAmount;
                string refund;
                while (true)
                {
                    Console.Write("Nhập số tiền khách thanh toán: ");
                    paymentAmount = staffBl.GetMoney();
                    if (Convert.ToDecimal(paymentAmount) >= order.total)
                    {
                        payment.paymentAmount = Convert.ToDecimal(paymentAmount);
                        paymentAmount = FormatCurrency(payment.paymentAmount.ToString());

                        payment.refund = payment.paymentAmount - order.total;
                        refund = FormatCurrency(payment.refund.ToString());
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Số tiền bạn nhập không đúng! Vui lòng nhập lại!");
                    }
                }
                HT1.Line(97);
                Console.WriteLine($"| + Tổng tiền      : {total,70} VND |");
                Console.WriteLine($"| + Tiền thanh toán: {paymentAmount,70} VND |");
                Console.WriteLine($"| + Hoàn tiền      : {refund,70} VND |");
                HT1.Line(97);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("|                            CẢM ƠN QUÝ KHÁCH VÀ KHÔNG MONG GẶP LẠI                             |");
            }
            else
            {
                Console.WriteLine("Tạo hoá đơn thất bại!");
            }
            WaitForButton("Nhập phím bất kỳ để tiếp tục...");
        }
        static string FormatCurrency(string currency)
        {
            for (int k = currency.Length - 3; k > 0; k = k - 3)
            {
                currency = currency.Insert(k, ".");
            }
            return currency;
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
        
    }
}