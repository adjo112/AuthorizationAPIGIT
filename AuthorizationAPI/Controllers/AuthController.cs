using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthorizationAPI.DBLayer;
using AuthorizationAPI.Models;
using AuthorizationAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;


namespace AuthorizationAPI.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserContext _context;
        private IConfiguration _config;
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(AuthController));
        private readonly IUserRepo repo;


        public AuthController(IConfiguration config, IUserRepo _repo, UserContext context)
        {
            _config = config;
            repo = _repo;
            _context = context;
        }

        /// <summary>
        /// Post method for Login
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>

        [HttpPost]
        public IActionResult Login([FromBody] User login)
        {
            AuthRepo auth_repo = new AuthRepo(_config, repo, _context);
            _log4net.Info("Login initiated!");
            IActionResult response = Unauthorized();
            //login.FullName = "user1";
            //  var user = auth_repo.AuthenticateUser(login);
            bool found_user = auth_repo.AuthenticateUser(login);
            if (found_user == false)
            {
                return NotFound();
            }
            else
            {
                var tokenString = auth_repo.GenerateJSONWebToken(login);
                response = Ok(new { token = tokenString });
            }

            return response;
        }



    }
}
