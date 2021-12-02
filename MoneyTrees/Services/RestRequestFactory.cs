using MoneyTrees.Interfaces;
using MoneyTrees.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Configuration;
using System.Globalization;

namespace MoneyTrees.Services
{
    public class RestRequestFactory : IRestRequestFactory
    {
        public RestRequest AuthorisationRequest(string url, Method method, string code)
        {
            RestRequest request = new RestRequest(url, method);

            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

            request.AddHeader("Accept", "application/json");

            request.AddParameter("application/x-www-form-urlencoded", "grant_type=authorization_code&client_id=" + ConfigurationManager.AppSettings["client_id"] +
            "&client_secret=" + RestSharp.Extensions.StringExtensions.UrlEncode(ConfigurationManager.AppSettings["client_secret"])
            + "&redirect_uri=" + ConfigurationManager.AppSettings["redirect_uri"] + "&code=" + code, ParameterType.RequestBody);

            return request;
        }

        public RestRequest GeneralRequest(string url, AccessTokenModel token)
        {
            RestRequest request = new RestRequest(url);

            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

            request.AddQueryParameter("expand[]", "merchant");

            request.AddHeader("Accept", "application/json");

            if (token != null)
            {
                request.AddHeader("Authorization", "Bearer " + token.AccessToken);
            }

            request.AddQueryParameter("account_id", ConfigurationManager.AppSettings["account_id"]);

            return request;
        }


        public RestRequest LogOutRequest(string url, Method method, AccessTokenModel token)
        {
            RestRequest request = new RestRequest(url, method);

            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

            request.AddHeader("Accept", "application/json");

            if (token != null)
            {
                request.AddHeader("Authorization", "Bearer " + token.AccessToken);
            }

            return request;
        }

        public RestRequest TransactionRequest(string url, AccessTokenModel token)
        { 
            
            RestRequest request = new RestRequest(url);

            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

            request.AddHeader("Accept", "application/json");

            request.AddQueryParameter("expand[]", "merchant");

            if (token != null)
            {
                request.AddHeader("Authorization", "Bearer " + token.AccessToken);
            }

            return request;

        }

        public RestRequest TransactionRequest90Days(string url, AccessTokenModel token)
        {
            RestRequest request = new RestRequest(url);

            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

  
            request.AddHeader("Accept", "application/json");

            if (token != null)
            {
                request.AddHeader("Authorization", "Bearer " + token.AccessToken);

            }

            request.AddQueryParameter("account_id", ConfigurationManager.AppSettings["account_id"]);

            request.AddQueryParameter("expand[]", "merchant");

            request.AddQueryParameter("since", DateTime.Now.AddDays(-90).ToString("yyyy-MM-dd'T'HH:mm:ss.fffzzz", DateTimeFormatInfo.InvariantInfo));

            return request;
        }


        public RestRequest RefreshRequest(string url, Method method, AccessTokenModel token)
        {

            RestRequest request = new RestRequest(url, method);

            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

            request.AddHeader("Accept", "application/json");


            if (token != null)
            {

                request.AddParameter("grant_type", "refresh_token", ParameterType.RequestBody);
                request.AddParameter("client_id", ConfigurationManager.AppSettings["client_id"], ParameterType.RequestBody);
                request.AddParameter("client_secret", RestSharp.Extensions.StringExtensions.UrlEncode(ConfigurationManager.AppSettings["client_secret"]), ParameterType.RequestBody);
                request.AddParameter("refresh_token", token.RefreshToken, ParameterType.HttpHeader);


            }

            return request;

        }
    }
}