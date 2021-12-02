using RestSharp;

namespace MoneyTrees.Interfaces
{
    public interface IRestClientFactory
    {
        RestClient Create();

    }
}
