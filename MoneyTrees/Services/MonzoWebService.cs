using MoneyTrees.DAL;
using MoneyTrees.Models;
using AutoMapper;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MoneyTrees.Interfaces
{
    public class MonzoWebService : IMonzoWebService
    {

        protected IRestClientFactory restClientFactory;
        protected IRestRequestFactory restRequestFactory;
        protected IMonzoRepository monzoRepository;
        protected IMonzoMapper mapper;



        private static readonly log4net.ILog log =  log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public MonzoWebService(IRestClientFactory restClientFactory, IRestRequestFactory restRequestFactory, IMonzoRepository monzoRepository, IMonzoMapper mapper)
        {
            this.restClientFactory = restClientFactory;
            this.restRequestFactory = restRequestFactory;
            this.monzoRepository = monzoRepository;
            this.mapper = mapper;

        }

        public async Task<AccessTokenModel> AuthorisationToken(string code)
        { 
            var client = restClientFactory.Create();

            var request = restRequestFactory.AuthorisationRequest("/oauth2/token", Method.POST, code);

            var response = client.Execute(request);

            if(response.IsSuccessful == false)
            {
                log.InfoFormat("Status = {0}, Successful = {1}, ResponseStatus = {2} ", response.StatusCode, response.IsSuccessful, response.ResponseStatus);

            }

            string jsonString = FormatJson(response.Content);

            var accessToken = JsonConvert.DeserializeObject<AccessTokenModel>(response.Content);

            await monzoRepository.InsertTokens(accessToken);

            await monzoRepository.DeleteTransactions();

            await GetTransactionsMonzo();

            return accessToken;
        }

        public async Task<BalanceModel> GetBalance()
        {
            var accessToken = await monzoRepository.GetToken();

            var client = restClientFactory.Create();

            var request = restRequestFactory.GeneralRequest("/balance", accessToken);

            var response = client.Execute(request);

            if (response.IsSuccessful == false)
            {
                log.InfoFormat("Status = {0}, Successful = {1}, ResponseStatus = {2} ", response.StatusCode, response.IsSuccessful, response.ResponseStatus);

            }

            string jsonString = FormatJson(response.Content);

            var balance = JsonConvert.DeserializeObject<BalanceModel>(response.Content);

            return balance;
        }



        public async Task GetTransactionsMonzo()
        {
            TransactionListModel transactions = new TransactionListModel();

            var accessToken = await monzoRepository.GetToken();

            var client = restClientFactory.Create();

            var request = restRequestFactory.GeneralRequest("/transactions", accessToken);

            IRestResponse response = client.Execute(request);

            if (response.IsSuccessful == false)
            {
                log.InfoFormat("Status = {0}, Successful = {1}, ResponseStatus = {2} ", response.StatusCode, response.IsSuccessful, response.ResponseStatus);

            }

            string jsonString = FormatJson(response.Content);

            transactions = JsonConvert.DeserializeObject<TransactionListModel>(response.Content);

            foreach(var item in transactions.transactions)
            {
                item.Amount = item.Amount / 100;
                await monzoRepository.InsertTransaction(item);
            }

        }

        public async Task<TransactionListModel> GetTransactionsDB()
        {
            TransactionListModel transactions = new TransactionListModel();


            transactions.transactions = mapper.Mapper().Map<List<Transaction>, List<TransactionModel>>(await monzoRepository.GetTransactionList());

         
            return transactions;
        }

        public async Task<TransactionListModel> GetTransactionsLast90Days()
        {
            
            var accessToken = await monzoRepository.GetToken();

            var client = restClientFactory.Create();

            var request = restRequestFactory.TransactionRequest90Days("/transactions", accessToken);

            var response = client.Execute(request);

            if (response.IsSuccessful == false)
            {
                log.InfoFormat("Status = {0}, Successful = {1}, ResponseStatus = {2} ", response.StatusCode, response.IsSuccessful, response.ResponseStatus);

            }

            string jsonString = FormatJson(response.Content);

            TransactionListModel transactions = JsonConvert.DeserializeObject<TransactionListModel>(response.Content);

            return transactions;
        }


        public async Task<SingleTransactionItemModel> GetAllTransaction(string transactionId)
        {
            var accessToken = await monzoRepository.GetToken();

            var client = restClientFactory.Create();

            var request = restRequestFactory.TransactionRequest("/transactions/" + transactionId, accessToken);

            var response = client.Execute(request);

            if (response.IsSuccessful == false)
            {
                log.InfoFormat("Status = {0}, Successful = {1}, ResponseStatus = {2} ", response.StatusCode, response.IsSuccessful, response.ResponseStatus);

            }

            string jsonString = FormatJson(response.Content);

            var transactions = JsonConvert.DeserializeObject<SingleTransactionItemModel>(response.Content);

            return transactions;
        }

        public async Task LogOut()
        {
            var accessToken = await monzoRepository.GetToken();

            var client = restClientFactory.Create();

            var request = restRequestFactory.LogOutRequest("/oauth2/logout", Method.POST, accessToken);

            var response = client.Execute(request);

            if (response.IsSuccessful == false)
            {
                log.InfoFormat("Status = {0}, Successful = {1}, ResponseStatus = {2} ", response.StatusCode, response.IsSuccessful, response.ResponseStatus);

            }

        }

        public async Task<AccessTokenModel> RefreshToken()
        {
            var accessToken = await monzoRepository.GetToken();

            var client = restClientFactory.Create();

            var request = restRequestFactory.RefreshRequest("/oauth2/token", Method.POST, accessToken);


            var response = client.Execute(request);

            if (response.IsSuccessful == false)
            {
                log.InfoFormat("Status = {0}, Successful = {1}, ResponseStatus = {2} ", response.StatusCode, response.IsSuccessful, response.ResponseStatus);

            }

            string jsonString = FormatJson(response.Content);

            AccessTokenModel AccessToken = JsonConvert.DeserializeObject<AccessTokenModel>(jsonString);           

            if (response.IsSuccessful == true)
            {
                await monzoRepository.InsertTokens(accessToken);

            }

            return AccessToken;
        }

        public async Task<WhoAmIModel> WhoAmI()
        {
            var accessToken = await monzoRepository.GetToken();

            var client = restClientFactory.Create();

            var request = restRequestFactory.GeneralRequest("/ping/whoami", accessToken);

            var response = client.Execute(request);

            if (response.IsSuccessful == false)
            {
                log.InfoFormat("Status = {0}, Successful = {1}, ResponseStatus = {2} ", response.StatusCode, response.IsSuccessful, response.ResponseStatus);

            }

            string jsonString = FormatJson(response.Content);

            var whoAmI = JsonConvert.DeserializeObject<WhoAmIModel>(response.Content);

            return whoAmI;
        }

        public async Task<AccountListModel> GetAccounts()
        {
            var accessToken = await monzoRepository.GetToken();

            var client = restClientFactory.Create();

            var request = restRequestFactory.GeneralRequest("/accounts", accessToken);

            var response = client.Execute(request);

            if (response.IsSuccessful == false)
            {
                log.InfoFormat("Status = {0}, Successful = {1}, ResponseStatus = {2} ", response.StatusCode, response.IsSuccessful, response.ResponseStatus);

            }

            string jsonString = FormatJson(response.Content);

            var accounts = JsonConvert.DeserializeObject<AccountListModel>(response.Content);

            return accounts;
        }

        private static string FormatJson(string json)
        {
            dynamic parsedJson = JsonConvert.DeserializeObject(json);

            return JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
        }
    }
}