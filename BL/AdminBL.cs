using codep.Model;
using codep.DAL;

namespace codep.BL
{
    public class AdminBL
    {
        private AdminDAL adminDAL = new AdminDAL();
        public AdminS LoginAdmin(AdminS admin)
        {
            return adminDAL.LoginAdmin(admin);
        }
    }
}