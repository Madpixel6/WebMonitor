using System.Net;

namespace WebMonitor.Models
{
    public class WebsiteStatus
    {
        public string Status { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccessStatusCode { get; set; }
    }
}
