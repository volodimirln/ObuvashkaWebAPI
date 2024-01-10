using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ObuvashkaWebAPI.Models;
using ObuvashkaWebAPI.Modules;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ObuvashkaWebAPI.Controllers
{
    [ApiController]
    public class OtherController : AProduct
    {
        public OtherController() : base() { }

        [HttpPost]
        [Route("v1/auth")]
        public ActionResult Authorization(string login, string password)
        {
            try
            {
                if (db.Administrarions.Any(p => p.Login == login && p.Password == password))
                {
                    var claims = new List<Claim> { new Claim(ClaimTypes.Role, db.Administrarions.FirstOrDefault(p => p.Login == login && p.Password == password).Name) };
                    var jwt = new JwtSecurityToken(
                            issuer: AuthOptions.ISSUER,
                            audience: AuthOptions.AUDIENCE,
                            claims: claims,
                            expires: DateTime.UtcNow.Add(TimeSpan.FromDays(365)),
                            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

                    return Ok(new JwtSecurityTokenHandler().WriteToken(jwt));
                }
                else
                    return StatusCode(401);
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpGet]
        [Route("v1/cnt/lastversion")]
        public ActionResult GetLastVersionCNT()
        {
            string file = Directory.GetFiles("wwwroot\\containers").LastOrDefault();

            return Ok(new { fileName = file.Split("\\")[2], url = "localhost/" + file.Split("\\")[2] });
        }

        [HttpGet]
        [Route("v1/gender")]
        public ActionResult<IEnumerable<Gender>> GetGenders() => GetData<Gender>();
 
        [HttpGet]
        [Route("v1/product/type")]
        public ActionResult<IEnumerable<Color>> GetColors() => GetData<Color>();
        
        [HttpGet]
        [Route("v1/order/sum/all")]
        [Authorize(Roles = "admin")]
        public ActionResult<IEnumerable<Color>> GetSumOrders() => Ok( new { sum = db.Orders.Sum(p => p.Sum)} );
        [HttpGet]
        [Route("v1/faq")]
        [Authorize(Roles = "admin")]
        public ActionResult<IEnumerable<Faq>> GetFAQ() => GetData<Faq>();
        [HttpGet]
        [Route("v1/order")]
        [Authorize(Roles = "admin")]
        public ActionResult<IEnumerable<Order>> GetOrder() => GetData<Order>();
        




    }
}
