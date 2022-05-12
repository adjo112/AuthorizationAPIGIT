using AuthorizationAPI.DBLayer;
using AuthorizationAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationAPI.Provider
{
    public interface IUserProvider
    {
       

        public bool UserPassExists(User cred,UserContext uc);

    }
}
