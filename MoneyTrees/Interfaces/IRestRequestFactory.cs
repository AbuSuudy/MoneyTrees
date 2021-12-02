using RestSharp;
using MoneyTrees.Models;

namespace MoneyTrees.Interfaces
{
    public interface IRestRequestFactory
    {
        RestRequest AuthorisationRequest(string url, Method method, string code);

        RestRequest GeneralRequest(string url,  AccessTokenModel token);

        RestRequest LogOutRequest(string url, Method method, AccessTokenModel token);

        RestRequest TransactionRequest(string url, AccessTokenModel token);

        RestRequest RefreshRequest(string url, Method method, AccessTokenModel token);

        RestRequest TransactionRequest90Days(string url, AccessTokenModel token);


    }
}
