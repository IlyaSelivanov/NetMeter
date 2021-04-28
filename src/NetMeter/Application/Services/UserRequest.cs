using RestSharp;

namespace Application.Services
{
    public class UserRequest
    {
        public string RequestResource { get; set; }
        public RestRequest RestRequest {get; set;}
    }
}
