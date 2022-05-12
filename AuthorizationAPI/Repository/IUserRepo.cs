using AuthorizationAPI.DBLayer;
using AuthorizationAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationAPI.Repository
{
    public interface IUserRepo
    {
        public bool GetUserCred(User cred,UserContext d);
    }
}
