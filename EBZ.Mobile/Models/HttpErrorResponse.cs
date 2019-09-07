using System.Net;

namespace EBZ.Mobile.Models
{
    public class HttpErrorResponse
    {
        public HttpStatusCode Code { get; set; }
        public string Message { get; set; }
        public string CustomMessage { get; set; }
    }
}
