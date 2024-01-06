using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ObuvashkaWebAPI.Models;
using System.Collections;
using System.Numerics;

namespace ObuvashkaWebAPI.Modules
{
    public interface  IProduct
    {
        public ActionResult GetImageProduct(int productId, bool status);
        public ActionResult GetAllImageProduct(int offset = 0, int limit = 100, bool sortDesc = false);
        public ActionResult GetVendorCodeProduct(string vendorCode);
        public ActionResult GetOpenPhotoPruduct<T>(int productId, int R = 255, int G = 255, int B = 255);
    }
}
