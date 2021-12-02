using System.Web.Configuration;
using MoneyTrees.Interfaces;
using RestSharp;

namespace MoneyTrees.Services
{
    public class RestClientFactory : IRestClientFactory
    {

        public RestClient Create()
        {
            RestClient client = new RestClient(WebConfigurationManager.AppSettings["endpoint"]);

            return client;
        }
    }
}