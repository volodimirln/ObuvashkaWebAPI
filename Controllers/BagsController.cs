using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ObuvashkaWebAPI.Models;
using ObuvashkaWebAPI.Modules;


namespace ObuvashkaWebAPI.Controllers
{
    [ApiController]
    public class BagsController : AProduct
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public BagsController(IWebHostEnvironment webHostEnvironment) : base()
        {
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpPost, DisableRequestSizeLimit, Route("v1/bags/image")]
        public IActionResult UploadImageProduct(IFormFile file, int productId, bool status) => UploadImageProduct<PictureToBag>(file, productId, status, _webHostEnvironment);

        [HttpDelete, Route("v1/bags/image")]
        public IActionResult DeleteImageProduct(int id)
        {
            var data = db.PictureToBags.FirstOrDefault(p => p.Id == id);
            var wwwrootPath = _webHostEnvironment.WebRootPath;
            var filePath = Path.Combine(wwwrootPath + "\\images", data.PhotoPath);
            System.IO.File.Delete(filePath);
            db.Remove(data);
            db.SaveChanges();
            return Ok();
        }

        [HttpGet]
        [Route("v1/images/{productId}")]
        public ActionResult GetImageProduct(int productId, bool status)
        {
            try
            {
                if (status == false && productId > 0)
                    return Ok(db.PictureToBags.Where(p => p.ProductId == productId && p.Status == status).OrderByDescending(p => p.Id).Include(r => r.Product));
                else if (productId < 0 || status == true)
                    return Ok(db.PictureToBags.Where(p => p.Product.Id == productId && p.Status == status).Include(r => r.Product));
                else
                    return NotFound();
            }
            catch
            {
                return StatusCode(500);
            }
        }
        [HttpPost]
        [Route("v1/bags")]
        public ActionResult AddProduct(Bag data) => AddData(data);

        [HttpPut]
        [Route("v1/bags")]
        public ActionResult EditProducr(Bag data) => EditData(data.Id, data);

        [HttpDelete]
        [Route("v1/bags/{id}")]
        public ActionResult DeleteProduct(int id) => DeleteData<Bag>(id);

        [HttpGet]
        [Route("v1/bags/images/all")]
        public ActionResult GetAllImageProduct(int offset = 0, int limit = 100, bool sortDesc = false) => GetDataRequest(db.PictureToBags.Skip(offset).Take(limit).Include(r => r.Product));
        

        [HttpGet]
        [Route("v1/bags/vendorcode")]
        public ActionResult GetVendorCodeProduct(string vendorCode) => GetDataRequest(db.Bags.Where(p => p.VendorCode == vendorCode));

        [HttpGet]
        [Route("v1/bags/images/color")]
        public ActionResult GetOpenPhotoPruduct<Bag>(int productId, int R = 255, int G = 255, int B = 255) => GetOpenPhoto<Bag>(productId, R, G, B);

    }
}
