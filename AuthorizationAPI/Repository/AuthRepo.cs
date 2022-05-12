using AuthorizationAPI.DBLayer;
using AuthorizationAPI.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationAPI.Repository
{
    public class AuthRepo
    {
        private readonly UserContext _context;
        private readonly IConfiguration _config;
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(AuthRepo));
        private readonly IUserRepo repo;
        public AuthRepo(IConfiguration config, IUserRepo _repo, UserContext context)
        {
            _config = config;
            repo = _repo;
            _context = context;
        }

     

        public string GenerateJSONWebToken(User userInfo) {
            _log4net.Info("Token Is Generated!");

            string userRole = "Customer";
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, userRole)
            };

            var token = new JwtSecurityToken(
             issuer: _config["Jwt:Issuer"],
             audience: _config["Jwt:Issuer"],
             claims: claims,
             expires: DateTime.Now.AddMinutes(30),
             signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);


        }



        public bool AuthenticateUser(User login)
        {
            _log4net.Info("Validating the User!");

            //Validate the User Credentials 
             return repo.GetUserCred(login, _context);

        }


    }
}



// here the userInfo is already existing in DB and verified
/*
public string GenerateJSONWebToken(User userInfo)
{
    _log4net.Info("Token Is Generated!");

    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(
      issuer: _config["Jwt:Issuer"],
      audience: _config["Jwt:Issuer"],
      null,
      expires: DateTime.Now.AddMinutes(30),
      signingCredentials: credentials);

    return new JwtSecurityTokenHandler().WriteToken(token);
}
*/
