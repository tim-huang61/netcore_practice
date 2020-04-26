using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Demo22_JWT.Controllers
{
    public class AccountController : Controller
    {
        // GET
        public async Task<IActionResult> CookieLogin(string userName)
        {
            var claimsIdentity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            claimsIdentity.AddClaim(new Claim("Name", userName));
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

            return Content("Login");
        }

        public async Task<ActionResult> JwtLogin([FromServices] SymmetricSecurityKey securityKey, string userName)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim("Name", userName));
            var token = new JwtSecurityToken(
                issuer: "localhost",
                audience: "localhost",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
            );

            var writeToken = new JwtSecurityTokenHandler().WriteToken(token);

            return Conflict(writeToken);
        }
    }
}