using codep.Model;

namespace codep.DAL
{
    public interface IOrderDAL
    {
        public bool CreateOrder(Orders order);
        public List<Orders> GetOr();
        //public List<Orders> GetAllPaidOrdersInDay();
    }
}