using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace Celin
{
    public class E1Service : AIS.Server
    {
        public E1Service(IConfiguration config, ILogger<E1Service> logger, IHttpClientFactory clientFactory)
            : base(config["BaseUrl"], logger, clientFactory.CreateClient())
        {
            AuthRequest.username = config["User"];
            AuthRequest.password = config["Password"];
        }
    }
}
