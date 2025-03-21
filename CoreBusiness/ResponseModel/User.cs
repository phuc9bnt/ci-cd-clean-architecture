using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreBusiness.ResponseModel
{
    public class User
    {
        public Guid UserID { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public int RoleID { get; set; }
        [ValidateNever]
        public Role Role { get; set; }
    }
}
