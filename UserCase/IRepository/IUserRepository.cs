using CoreBusiness.LoginModel;
using CoreBusiness.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserCase.IRepository
{
    public interface IUserRepository
    {
        public User GetUserByLoginRequest(UserLogin user);
    }
}
