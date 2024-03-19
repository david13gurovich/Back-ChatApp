using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
//using API.Data;
using Domain;
using API.Data;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class usersController : Controller
    {
        private readonly PomeloDB _context;
        private readonly IConfiguration conf;
        public usersController(PomeloDB context, IConfiguration conf)
        {
            _context = context;
            this.conf = conf;
        }

        [HttpGet]
        [Route("/api/users")]
        public async Task<ActionResult> Index()
        {
            //return just the name without other field
            return Json(_context.User);
        }
        //in the log in take the user and make token

        [HttpPost]
        [Route("/api/users/signin")]
        public async Task<ActionResult> SignIn([FromBody] SignInRequest request)
        {
            var usersList = await _context.User.Where(user => user.UserName == request.UserId).ToListAsync();
            var user = usersList.FirstOrDefault();
            if (user == null) return NotFound();
            if (request.Password != user.Password) return NotFound();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, conf["JWT:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("UserId", request.UserId.ToString()),
            };
            var symKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(conf["JWT:Key"]));
            var signInCreds = new SigningCredentials(symKey, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                conf["JWT:Issuer"],
                conf["JWT:Audience"],
                claims,
                expires: DateTime.UtcNow.AddDays(2),
                signingCredentials: signInCreds);
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
 
            return Ok(new ResponseLogin() { Token = jwtToken });

        }

        //add new user before token

        [HttpPost]
        [Route("/api/users")]
        public async Task<ActionResult> AddNewUser([FromBody] User user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();
            //return just the name without other field
            return Json(_context.User);
        }


       
    }
}
