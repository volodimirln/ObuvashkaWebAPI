using ObuvashkaWebAPI.Models;

namespace ObuvashkaWebAPI.Modules.Request
{
    public class RequestShoes
    {
        public string sortField { set; get; } = "Id";
        public bool sortDesc { set; get; }
        public int limit { set; get; }
        public int offset { set; get; }
        public string vendorCode { set; get; }
    }
}
