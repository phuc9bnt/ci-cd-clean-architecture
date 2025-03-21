using CoreBusiness.LoginModel;
using CoreBusiness.ResponseModel;
using Microsoft.EntityFrameworkCore;
using UserCase.IRepository;

namespace SQLServerPlugin.Repositories
{
    public class UserRepository(CustomDBContext _dbContext) : IUserRepository
    {
        public User GetUserByLoginRequest(UserLogin user)
        {
            return _dbContext.Users.Include(x => x.Role)
                .Where(y => y.UserName == user.UserName && y.Password == user.Password)
                .FirstOrDefault();
        }
    }
}
