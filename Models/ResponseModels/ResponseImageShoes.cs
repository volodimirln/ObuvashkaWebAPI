using ObuvashkaWebAPI.Models;

namespace ObuvashkaWebAPI.Models.ResponseModels
{
    public class ResponseImageShoes
    {
        public string id { get; set; }
        public string productId { get; set; }
        public string photoPath { get; set; }
        public string status { get; set; }
        public string vendorCode { get; set; }

        public ResponseImageShoes(PictureToProduct image) 
        {
            id = image.Id.ToString();
            productId = image.ProductId.ToString();
            photoPath = image.PhotoPath.ToString();
            status = image.Status.ToString();
            Cx07681BillingContext context = new ();
            vendorCode = context.Shoes.Find(image.ProductId).VendorCode;
        }
    }
}
