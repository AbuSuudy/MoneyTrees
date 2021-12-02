using MoneyTrees.DAL;
using MoneyTrees.Interfaces;
using MoneyTrees.Models;
using MoneyTrees.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI.WebControls.Expressions;

namespace MoneyTrees.Controllers
{
    public class HomeController : Controller
    {
        protected IMonzoWebService monzoWebService;
        protected IMonzoRepository monzoRepository;


        public HomeController(IMonzoWebService monzoWebService, IMonzoRepository monzoRepository)
        {
            this.monzoRepository = monzoRepository;
            this.monzoWebService = monzoWebService;
       
        }



        [HttpGet]
        public async Task<ActionResult> Index(string code)
        {

            if (code != null)
            {

                await monzoWebService.AuthorisationToken(code);

                return RedirectToAction("Transactions");

            }

            return View();
        }

        public async Task<ActionResult> Visulisation()
        {
           List<List<double>> sumOfEachDays =  await monzoRepository.SumOfEachDay();

            VisualisationCalculation visualisationCalculation = await monzoRepository.GetTransactionInfo();

            List<CategorySum> catsum = await monzoRepository.SumOfEachCategory();

            VisualisationModel visualisation = new VisualisationModel
            {
                VisualisationCalculation = visualisationCalculation,
                StockChartData = sumOfEachDays,
                CategorySums = catsum
            };

            return View(visualisation);
        }

        public async Task<ActionResult> Transactions()
        {


            var transaction = await monzoWebService.GetTransactionsDB();

            AccountSummaryViewModel accountSummaryViewModel = new AccountSummaryViewModel();
  

            var balance = await monzoWebService.GetBalance();
            var account = await monzoWebService.GetAccounts();

            accountSummaryViewModel.Balance = balance.Balance;
            accountSummaryViewModel.SpendToday = balance.SpendToday;

            if (account.accounts != null)
            {
                accountSummaryViewModel.AccountHolderName = account.accounts.ElementAt(0).Owners.ElementAt(0).preferred_name;
            }

            if (transaction.transactions != null)
            {
                accountSummaryViewModel.TransactionLocation = transaction.transactions.Where(x => x.Merchant != null).Select(x => new TransactionLocation { Longitude = Math.Round(x.Merchant.Address.Longitude, 6), Latitude = Math.Round(x.Merchant.Address.Latitude, 6), Name = x.Merchant.Name }).ToList();

            }

            return View(accountSummaryViewModel);

        }


        public static async Task<AccessTokenModel> HttpClientAPI(string code)
        {
            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri("https://api.monzo.com")
            };

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            FormUrlEncodedContent content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "authorization_code"),
                new KeyValuePair<string, string>("client_id", ConfigurationManager.AppSettings["client_id"]),
                new KeyValuePair<string, string>("client_secret", ConfigurationManager.AppSettings["client_secret"]),
                new KeyValuePair<string, string>("redirect_uri", ConfigurationManager.AppSettings["redirect_uri"]),
                new KeyValuePair<string, string>("code", code)

            });

            Task<HttpResponseMessage> result = client.PostAsync("/oauth2/token", content);

            string jsonString = await result.Result.Content.ReadAsStringAsync();

            AccessTokenModel AccessToken = JsonConvert.DeserializeObject<AccessTokenModel>(jsonString);

            return AccessToken;

        }

    }
}