using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ObuvashkaWebAPI.Models;
using ObuvashkaWebAPI.Modules;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ObuvashkaWebAPI.Controllers
{
    [ApiController]
    public class AccessoriesController : AProduct
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AccessoriesController(IWebHostEnvironment webHostEnvironment) : base()
        {
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpPost, DisableRequestSizeLimit, Route("v1/accessories/image")]
        [Authorize(Roles = "admin")]
        public IActionResult UploadImageProduct(IFormFile file, int productId, bool status) => UploadImageProduct<PictureToAccessory>(file, productId, status, _webHostEnvironment);

        [HttpDelete, Route("v1/accessories/image")]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteImageProduct(int id)
        {
            var data = db.PictureToAccessories.FirstOrDefault(p => p.Id == id);
            var wwwrootPath = _webHostEnvironment.WebRootPath;
            var filePath = Path.Combine(wwwrootPath + "\\images", data.PhotoPath);
            System.IO.File.Delete(filePath);
            db.Remove(data);
            db.SaveChanges();
            return Ok();
        }
        [HttpGet]
        [Route("v1/accessories/images/{productId}")]
        public  ActionResult GetImageProduct(int productId, bool status)
        {
            try
            {
                if (status == false && productId > 0)
                    return Ok(db.PictureToAccessories.Where(p => p.ProductId == productId && p.Status == status).OrderByDescending(p => p.Id).Include(r => r.Product));
                else if (productId < 0 || status == true)
                    return Ok(db.PictureToAccessories.Where(p => p.Product.Id == productId && p.Status == status).Include(r => r.Product));
                else
                    return NotFound();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Route("v1/accessories")]
        [Authorize(Roles = "admin")]
        public ActionResult AddProduct(Accessory data) => AddData(data);

        [HttpPut]
        [Route("v1/accessories")]
        [Authorize(Roles = "admin")]
        public ActionResult EditProducr(Accessory data) => EditData(data.Id, data);

        [HttpDelete]
        [Route("v1/accessories/{id}")]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteProduct(int id) => DeleteData<Accessory>(id);
        [HttpGet]
        [Route("v1/accessories/images/all")]
        public ActionResult GetAllImageProduct(int offset = 0, int limit = 100, bool sortDesc = false) => GetDataRequest(db.PictureToAccessories.Skip(offset).Take(limit).Include(r => r.Product));
        
        [HttpGet]
        [Route("v1/accessories/vendorcode")]
        public ActionResult GetVendorCodeProduct(string vendorCode) => GetDataRequest(db.Accessories.Where(p => p.VendorCode == vendorCode));

        [HttpGet]
        [Route("v1/accessories/images/color")]
        public ActionResult GetOpenPhotoPruduct<Accessory>(int productId, int R = 255, int G = 255, int B = 255) => GetOpenPhoto<Accessory>(productId, R, G, B);
    }
}
