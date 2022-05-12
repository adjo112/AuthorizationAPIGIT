using AuthorizationAPI.DBLayer;
using AuthorizationAPI.Models;
using AuthorizationAPI.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationAPI.Repository
{
    public class UserRepo : IUserRepo
    {
        private IUserProvider provider;


            public UserRepo(IUserProvider _provider)
            {
                provider = _provider;
            }
        

        public bool GetUserCred(User cred,UserContext d)
        {
            if (cred == null)
            {
                return false;
            }

            return provider.UserPassExists(cred,d);

        }
    }

}

