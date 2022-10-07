using codep.Model;

namespace codep.DAL
{
    public interface IStaffDAL
    {
         public Staff Login(Staff staff);
         public int RegUser(Staff staff);
    }
}