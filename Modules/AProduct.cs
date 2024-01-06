using Elfie.Serialization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using ObuvashkaWebAPI.Models;
using System.Net;
using System.Reflection;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ObuvashkaWebAPI.Modules
{

    public abstract class AProduct : Controller
    {
        protected readonly Cx07681BillingContext db;
        protected readonly ServiceAPI service;


        protected AProduct()
        {
            db = new Cx07681BillingContext();
            service = new ServiceAPI();
        }
        [NonAction]

        public ActionResult<IEnumerable<T>> GetData<T>()
        {
            IEnumerable<T> data = service.GetData<T>(db);
            if(data.Any())
                return Ok(data);
            else
                return NotFound();
        }
        [NonAction]

        public ActionResult GetDataRequest<T>(IEnumerable<T> values)
        {
            try
            {
                if (values.Any())
                    return Ok(values);
                else
                    return NotFound();
            }
            catch
            {
                return StatusCode(500);
            }
        }
        [NonAction]

        public virtual ActionResult GetOpenPhoto<T>(int productId, int R = 255, int G = 255, int B = 255)
        {
            string filename = "";
            if (typeof(T) == typeof(PictureToProduct))
                filename = db.PictureToProducts.FirstOrDefault(p => p.Status == true && p.ProductId == productId).PhotoPath;
            else if (typeof(T) == typeof(PictureToBag))
                filename = db.PictureToBags.FirstOrDefault(p => p.Status == true && p.ProductId == productId).PhotoPath;
            else if (typeof(T) == typeof(PictureToAccessory))
                filename = db.PictureToAccessories.FirstOrDefault(p => p.Status == true && p.ProductId == productId).PhotoPath;
            else
                return NotFound();

            WebClient client = new();
            byte[] imageData = client.DownloadData("https://obuvashka23.ru/img/?filename=" + filename + "&R=" + R + "&G=" + G + "&B=" + B);
            return File(imageData, "image/png");
        }
        [NonAction]
        public ActionResult AddData(object model)
        {
            try
            {
                if (model == null)
                    return NoContent();
                else
                    db.Add(model);
                db.SaveChanges();
                return CreatedAtRoute("default", model);

            }
            catch
            {
                return StatusCode(500);
            }
        }
        [NonAction]
        public ActionResult EditData<T>(int id, T sources)
        {
            try
            {
                PropertyInfo property = typeof(T).GetProperty("Id");
                T data = service.GetData<T>(db).FirstOrDefault(x => property.GetValue(x, null)?.ToString() == id.ToString());
                db.Entry(data).CurrentValues.SetValues(sources);
                db.SaveChanges();
                return Ok(sources);
            }
            catch
            {
                return StatusCode(500);
            }
        }
        [NonAction]
        public ActionResult DeleteData<T>(int id)
        {
            try
            {
                PropertyInfo property = typeof(T).GetProperty("Id");
                T data = service.GetData<T>(db).FirstOrDefault(x => property.GetValue(x, null)?.ToString() == id.ToString());
                db.Remove(data);
                db.SaveChanges();
                return Ok(new {message = "Data is deleted"});
            }
            catch
            {
                return StatusCode(500);
            }
        }
        [NonAction]
        public virtual IActionResult UploadImageProduct<T>(IFormFile file, int productId, bool status, IWebHostEnvironment environment)
        {
            try
            {
                var wwwrootPath = environment.WebRootPath;
                string customName = "";
                int countPhoto = 0; 
               System.Type type = typeof(T);

                if (type == typeof(PictureToProduct))
                {
                    customName = db.PictureToProducts.Where(p => p.ProductId == productId).Include(p => p.Product).FirstOrDefault().Product.VendorCode;
                    countPhoto = db.PictureToProducts.Where(p => p.ProductId == productId).Include(p => p.Product).Count();
                }
                else if (type == typeof(PictureToAccessory))
                {
                    customName = db.PictureToAccessories.Where(p => p.ProductId == productId).Include(p => p.Product).FirstOrDefault().Product.VendorCode;
                    countPhoto = db.PictureToAccessories.Where(p => p.ProductId == productId).Include(p => p.Product).Count();
                }
                else if (type == typeof(PictureToBag))
                {
                    customName = db.PictureToBags.Where(p => p.ProductId == productId).Include(p => p.Product).FirstOrDefault().Product.VendorCode;
                    countPhoto = db.PictureToBags.Where(p => p.ProductId == productId).Include(p => p.Product).Count();
                }
                else { 
                    return NotFound();
                }

                if (countPhoto > 0)
                {
                    countPhoto = countPhoto + 1;
                    customName = customName + "_" + countPhoto;
                }
                customName = customName + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(wwwrootPath + "\\images", customName);

                using (var fstream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fstream);
                    db.Add(new PictureToProduct() { PhotoPath = customName, ProductId = productId, Status = status });
                    db.SaveChanges();
                    return Ok();
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
