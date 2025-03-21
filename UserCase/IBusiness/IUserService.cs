using CoreBusiness.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserCase.IBusiness
{
    public interface IUserService
    {
        public string GenerateToken(User user);
        public User GetUserByUserName(string userName);
    }
}
