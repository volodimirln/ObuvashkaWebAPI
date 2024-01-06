using Microsoft.AspNetCore.Mvc;
using ObuvashkaWebAPI.Models;
using ObuvashkaWebAPI.Modules;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ObuvashkaWebAPI.Controllers
{
    [ApiController]
    public class OtherController : AProduct
    {
        public OtherController() : base() { }

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
        [Route("v1/token/test")]
        public ActionResult token() => Ok(this.Request.Headers["token"]);
        
        [HttpGet]
        [Route("v1/order/sum/all")]
        public ActionResult<IEnumerable<Color>> GetSumOrders() => Ok( new { sum = db.Orders.Sum(p => p.Sum)} );
        [HttpPost]
        [Route("v1/faq")]
        public ActionResult<IEnumerable<Faq>> GetFAQ() => GetData<Faq>();
        [HttpPost]
        [Route("v1/order")]
        public ActionResult<IEnumerable<Order>> GetOrder() => GetData<Order>();
        




    }
}
