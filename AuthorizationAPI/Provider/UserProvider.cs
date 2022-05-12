using AuthorizationAPI.DBLayer;
using AuthorizationAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationAPI.Provider
{
    public class UserProvider : IUserProvider
    {
        public bool UserPassExists(User cred, UserContext _context)
        {
            try
            {

                var t_user = _context.Users.FirstOrDefault(e => e.UserName == cred.UserName);
                var t_pass = _context.Users.FirstOrDefault(e => e.Password == cred.Password);
                if (t_user != null && t_pass != null)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e) { }
            return false; 
        }
    }
}




//  return _context.Users.Any(e => e.UserName == cred.UserName) && _context.Users.Any(e => e.Password == cred.Password);
//        var User = _userContext.Users
//                               .FirstOrDefault(u => u.Username == data.UserName);