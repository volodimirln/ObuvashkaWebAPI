using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ObuvashkaWebAPI.Models;
using System.Reflection;
using ObuvashkaWebAPI.Modules;
using Microsoft.CodeAnalysis;
using Microsoft.AspNetCore.Hosting;
using System.Security.AccessControl;
using Microsoft.AspNetCore.Authorization;

namespace ObuvashkaWebAPI.Controllers
{
    [ApiController]
    public class ShoesController : AProduct, IProduct
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ShoesController(IWebHostEnvironment webHostEnvironment) : base()
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]//!!!!!!!!!!!!!!!!
        [Route("v1/shoes")]
        [ProducesResponseType(typeof(Shoe), 200)]
        public ActionResult GetProduct(bool sortDesc, int offset = 0, int limit = 100, string sortField = "Id", string vendorCode = "")
        {
            PropertyInfo property = typeof(Shoe).GetProperty(sortField);//первая буква всегда должна быть большой
            List<Shoe> data = db.Shoes.ToList();
            if(!string.IsNullOrEmpty(vendorCode))
                data = data.Where(p => p.VendorCode == vendorCode).ToList();
            if(offset != null)
                data = data.Skip(offset).ToList();
            if(limit != null)
                data = data.Take(limit).ToList();
            if(sortDesc)
                data = data.OrderByDescending(x => property.GetValue(x, null)).ToList();
            else
                data = data.OrderBy(x => property.GetValue(x, null)).ToList();

            return Ok(data);
        }
        [HttpGet]//!!!!!!!!!!!!!!!!
        [Route("v1/shoes/filter")]
        [ProducesResponseType(typeof(Shoe), 200)]
        public ActionResult GetProductFilter(bool sortDesc, 
            int brand = 0, int gender = 0, int season = 0, int typeShoes = 0, int priceUp = 0, int priceDown = 0, int discountUp = 0, int discountDown = 0, bool markdown = false, 
            int offset = 0, int limit = 0, bool count = false, string sortField = "Id", string vendorCode = "")
        {
            PropertyInfo property = typeof(Shoe).GetProperty(sortField);//первая буква всегда должна быть большой
            List<Shoe> data = db.Shoes.ToList();
            if (!string.IsNullOrEmpty(vendorCode))
                data = data.Where(p => p.VendorCode == vendorCode).ToList();
            if (offset != 0)
                data = data.Skip(offset).ToList();
            if (limit != 0)
                data = data.Take(limit).ToList();
            if (brand != 0)
                data = data.Where(p =>p.Brand == brand).ToList();
            if(gender != 0)
                data = data.Where(p => p.GenderId == gender).ToList();
            if (typeShoes != 0)
                data = data.Where(p => p.Type.Id == typeShoes).ToList();
            if (priceUp != 0)
                data = data.Where(p => p.Price <= priceUp).ToList();
            if (priceDown != 0)
                data = data.Where(p => p.Price >= priceDown).ToList();
            if (discountUp != 0)
                data = data.Where(p => p.Discount <= discountUp).ToList();
            if (discountDown != 0)
                data = data.Where(p => p.Discount >= discountDown).ToList();
            if(markdown)
                data = data.Where(p => p.Markdown).ToList();

            if (sortDesc)
                data = data.OrderByDescending(x => property.GetValue(x, null)).ToList();
            else
                data = data.OrderBy(x => property.GetValue(x, null)).ToList();

            if(!count)
                return Ok(data);
            else
                return Ok(new {count = data.Count});
        }

        [HttpPost, DisableRequestSizeLimit, Route("v1/shoes/image")]
        [Authorize(Roles = "admin")]
        public IActionResult UploadImageProduct(IFormFile file, int productId, bool status) => UploadImageProduct<PictureToProduct>(file, productId, status, _webHostEnvironment);

        [HttpDelete, DisableRequestSizeLimit, Route("v1/shoes/image")]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteImageProduct(int id)
        {
            var data = db.PictureToProducts.FirstOrDefault(p =>p.Id == id);
            var wwwrootPath = _webHostEnvironment.WebRootPath;
            var filePath = Path.Combine(wwwrootPath + "\\images", data.PhotoPath);
            System.IO.File.Delete(filePath);
            db.Remove(data);
            db.SaveChanges();
            return Ok();
        }
        /// <summary>
        /// Список фотографий обуви
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// status - true - главная фотография обуви,
        /// status -  false - другие фотографии обуви
        /// </remarks>
        /// 
        [HttpGet]
        [Route("v1/shoes/images/{productId}")]
        [ProducesResponseType(typeof(PictureToProduct), 200)]
        public ActionResult GetImageProduct(int productId, bool status)
        {
            try
            {
                if (status == false && productId > 0)
                    return Ok(db.PictureToProducts.Where(p => p.ProductId == productId && p.Status == status).OrderByDescending(p => p.Id).Include(r => r.Product));
                else if (productId < 0 || status == true)
                    return Ok(db.PictureToProducts.Where(p => p.Product.Id == productId && p.Status == status).Include(r => r.Product));
                else
                    return NotFound();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("v1/shoes/size/all")]
        public ActionResult GetSize(bool sortDesc = false, int limit = 100, int offset = 0, string field = "id") => GetDataRequest(service.SortList(db.Sizes, field, sortDesc, limit, offset));

        [HttpPost]
        [Route("v1/shoes")]
        [Authorize(Roles = "admin")]
        public ActionResult AddProduct(Shoe data) => AddData(data);

        [HttpPut]
        [Route("v1/shoes")]
        [Authorize(Roles = "admin")]
        public ActionResult EditProducr(Shoe data) => EditData(data.Id, data);

        [HttpDelete]
        [Route("v1/shoes/{id}")]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteProduct(int id) => DeleteData<Shoe>(id);

        //!!!
        [HttpGet]
        [Route("v1/shoes/images/all")]
        [ProducesResponseType(typeof(IEnumerable<PictureToProduct>), 200)]
        public ActionResult GetAllImageProduct(int offset = 0, int limit = 100, bool sortDesc = false) => GetDataRequest(db.PictureToProducts.Skip(offset).Take(limit).Include(r => r.Product));

        [HttpGet]
        [Route("v1/shoes/colors")]
        public ActionResult<IEnumerable<Color>> GetColors() => GetData<Color>();

        [HttpPost]
        [Route("v1/shoes/colors")]
        [Authorize(Roles = "admin")]
        public ActionResult AddColors(Color data) => AddData(data);

        [HttpPut]
        [Route("v1/shoes/colors")]
        [Authorize(Roles = "admin")]
        public ActionResult EditColors(Color data) => EditData(data.Id, data);

        [HttpDelete]
        [Route("v1/shoes/colors/{id}")]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteColor(int id) => DeleteData<Color>(id);

        [HttpGet]
        [Route("v1/shoes/tnved")]
        public ActionResult<IEnumerable<Tnved>> GetTNVED() => GetData<Tnved>();
        [HttpPost]
        [Route("v1/shoes/tnved")]
        [Authorize(Roles = "admin")]
        public ActionResult AddTnved(Tnved data) => AddData(data);
        [HttpPut]
        [Route("v1/shoes/tnved")]
        [Authorize(Roles = "admin")]
        public ActionResult EditTnved(Tnved data) => EditData(data.Id, data);
        [HttpDelete]
        [Route("v1/shoes/tnved/{id}")]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteTnved(int id) => DeleteData<Tnved>(id);
        /***/

        [HttpGet]
        [Route("v1/shoes/countries/brands")]
        public ActionResult<IEnumerable<CountryBrand>> GetCountryBrands() => GetData<CountryBrand>();
        [HttpPost]
        [Route("v1/shoes/countries/brands")]
        [Authorize(Roles = "admin")]
        public ActionResult AddCountryBrands(CountryBrand data) => AddData(data);
        [HttpPut]
        [Route("v1/shoes/countries/brands")]
        [Authorize(Roles = "admin")]
        public ActionResult EditCountryBrands(CountryBrand data) => EditData(data.Id, data);
        [HttpDelete]
        [Route("v1/shoes/countries/brands/{id}")]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteCountryBrands(int id) => DeleteData<CountryBrand>(id);
        /***/
        [HttpGet]
        [Route("v1/shoes/countries")]
        public ActionResult<IEnumerable<Country>> GetCountries() => GetData<Country>();

        [Authorize(Roles = "admin")]
        [HttpPost]
        [Route("v1/shoes/countries")]
        [Authorize(Roles = "admin")]
        public ActionResult AddCountries(Country data) => AddData(data);
        [HttpPut]
        [Route("v1/shoes/countries")]
        [Authorize(Roles = "admin")]
        public ActionResult EditCountries(Country data) => EditData(data.Id, data);
        [HttpDelete]
        [Route("v1/shoes/countries/{id}")]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteCountries(int id) => DeleteData<Country>(id);
        /***/
        [HttpGet]
        [Route("v1/shoes/stylies")]
        public ActionResult<IEnumerable<Style>> GetStylies() => GetData<Style>();
        [HttpPost]
        [Route("v1/shoes/stylies")]
        [Authorize(Roles = "admin")]
        public ActionResult AddStylies(Style data) => AddData(data);
        [HttpPut]
        [Route("v1/shoes/stylies")]
        [Authorize(Roles = "admin")]
        public ActionResult EditStylies(Style data) => EditData(data.Id, data);
        [HttpDelete]
        [Route("v1/shoes/stylies/{id}")]
        [Authorize(Roles = "admin")]
        public ActionResult Deletestylies(int id) => DeleteData<Style>(id);
        /***/
        [HttpGet]
        [Route("v1/shoes/outmaterials")]
        public ActionResult<IEnumerable<Outmaterial>> GetOutmaterials() => GetData<Outmaterial>();
        [HttpPost]
        [Route("v1/shoes/outmaterials")]
        [Authorize(Roles = "admin")]
        public ActionResult AddOutmaterials(Outmaterial data) => AddData(data);
        [HttpPut]
        [Route("v1/shoes/outmaterials")]
        [Authorize(Roles = "admin")]
        public ActionResult EditOutmaterials(Outmaterial data) => EditData(data.Id, data);
        [HttpDelete]
        [Route("v1/shoes/outmaterials/{id}")]
        [Authorize(Roles = "admin")]
        public ActionResult Deleteoutmaterials(int id) => DeleteData<Outmaterial>(id);
        /***/
        [HttpGet]
        [Route("v1/shoes/insolematerial")]
        public ActionResult<IEnumerable<InsoleMaterial>> GetInsoleMaterial() => GetData<InsoleMaterial>();
        [HttpPost]
        [Route("v1/shoes/insolematerial")]
        [Authorize(Roles = "admin")]
        public ActionResult AddInsolematerial(InsoleMaterial data) => AddData(data);
        [HttpPut]
        [Route("v1/shoes/insolematerial")]
        [Authorize(Roles = "admin")]
        public ActionResult EditInsolematerial(InsoleMaterial data) => EditData(data.Id, data);
        [HttpDelete]
        [Route("v1/shoes/insolematerial/{id}")]
        [Authorize(Roles = "admin")]
        public ActionResult Deleteinsolematerial(int id) => DeleteData<InsoleMaterial>(id);
        /***/
        [HttpGet]
        [Route("v1/shoes/brands")]
        public ActionResult<IEnumerable<Brand>> GetBrands() => GetData<Brand>();
        [HttpPost]
        [Route("v1/shoes/brands")]
        [Authorize(Roles = "admin")]
        public ActionResult AddBrands(Brand data) => AddData(data);
        [HttpPut]
        [Route("v1/shoes/brands")]
        [Authorize(Roles = "admin")]
        public ActionResult EditBrand(Brand data) => EditData(data.Id, data);
        [HttpDelete]
        [Route("v1/shoes/brands/{id}")]
        [Authorize(Roles = "admin")]
        public ActionResult Deletebrands(int id) => DeleteData<Brand>(id);
        /***/
        [HttpGet]
        [Route("v1/shoes/materials")]
        public ActionResult<IEnumerable<Material>> GetMaterials() => GetData<Material>();
        [HttpPost]
        [Route("v1/shoes/materials")]
        [Authorize(Roles = "admin")]
        public ActionResult AddMaterials(Material data) => AddData(data);
        [HttpPut]
        [Route("v1/shoes/materials")]
        [Authorize(Roles = "admin")]
        public ActionResult EditMaterials(Material data) => EditData(data.Id, data);
        [HttpDelete]
        [Route("v1/shoes/materials/{id}")]
        [Authorize(Roles = "admin")]
        public ActionResult Deletematerials(int id) => DeleteData<Material>(id);
        /***/
        [HttpGet]
        [Route("v1/shoes/seasons")]
        public ActionResult<IEnumerable<Season>> GetSeasons() => GetData<Season>();
        [HttpPost]
        [Route("v1/shoes/seasons")]
        [Authorize(Roles = "admin")]
        public ActionResult AddSeasons(Season data) => AddData(data);
        [HttpPut]
        [Route("v1/shoes/seasons")]
        [Authorize(Roles = "admin")]
        public ActionResult EditSeasons(Season data) => EditData(data.Id, data);
        [HttpDelete]
        [Route("v1/shoes/seasons/{id}")]
        [Authorize(Roles = "admin")]
        public ActionResult Deleteseasons(int id) => DeleteData<Season>(id);
        /***/
        [HttpGet]
        [Route("v1/shoes/size")]
        public ActionResult<IEnumerable<Size>> GetSize() => GetData<Size>();
        [HttpPost]
        [Route("v1/shoes/size")]
        [Authorize(Roles = "admin")]
        public ActionResult AddSize(Size data) => AddData(data);
        [HttpPut]
        [Route("v1/shoes/size")]
        [Authorize(Roles = "admin")]
        public ActionResult EditSize(Size data) => EditData(data.Id, data);
        [HttpDelete]
        [Route("v1/shoes/size/{id}")]
        [Authorize(Roles = "admin")]

        public ActionResult Deletesize(int id) => DeleteData<Size>(id);
        /***/
        [HttpGet("v1/shoes/vendorcode"), ProducesResponseType(typeof(IEnumerable<Shoe>), 200)]
        public ActionResult GetVendorCodeProduct(string vendorCode) =>  GetDataRequest(db.Shoes.Where(p => p.VendorCode == vendorCode));
        [HttpGet]
        [Route("v1/shoes/images/color")]
        public ActionResult GetOpenPhotoPruduct<Shoe>(int productId, int R = 255, int G = 255, int B = 255) => GetOpenPhoto<Shoe>(productId, R, G, B);
    }
}
