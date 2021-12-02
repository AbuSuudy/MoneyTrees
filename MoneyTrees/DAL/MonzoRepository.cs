using MoneyTrees.Interfaces;
using MoneyTrees.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyTrees.DAL
{
    public class MonzoRepository : IMonzoRepository
    {

        private IMonzoMapper helper;
        private Token source;
        public static AccessTokenModel Globaltoken;

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public MonzoRepository(IMonzoMapper helper, Token source)
        {

            this.helper = helper;
            this.source = source;
        }

        public async Task<AccessTokenModel> GetToken()
        {

            AccessTokenModel destination = null;

            try
            {

                using (var context = new MonzoEntities())
                {

                    source = await context.Tokens.OrderByDescending(x => x.CreationDate).FirstOrDefaultAsync();
                }


                destination = helper.Mapper().Map<Token, AccessTokenModel>(source);


            }
            catch (Exception ex)
            {
                log.Error(ex.Message);

            }


            return destination;


        }

        public async Task InsertTokens(AccessTokenModel token)
        {
            try
            {


                    Token destination = helper.Mapper().Map<AccessTokenModel, Token>(token);

                    destination.CreationDate = DateTime.Now;

                    using (var context = new MonzoEntities())
                    {

                        context.Tokens.Add(destination);

                        await context.SaveChangesAsync();

                    }




            }
            catch (Exception ex)
            {
                log.Error(ex.Message);

            }
        }


        public async Task<List<Transaction>> GetTransactionList()
        {
            List<Transaction> transactions = new List<Transaction>();

            using (var context = new MonzoEntities())
            {

                transactions = await context.Transactions.ToListAsync();

            }
            return transactions;
        }

        public async Task InsertTransaction(TransactionModel transaction)
        {

            Transaction destination = helper.Mapper().Map<TransactionModel, Transaction>(transaction);

            if (transaction.Merchant != null)
            {
                destination.Name = transaction.Merchant.Name;
                destination.Address = transaction.Merchant.Address.Address;
                destination.Country = transaction.Merchant.Address.Country;
                destination.Longitude = transaction.Merchant.Address.Longitude;
                destination.Latitude = transaction.Merchant.Address.Latitude;
                destination.Logo = transaction.Merchant.Logo;
                destination.Emoji = transaction.Merchant.Emoji;
                destination.City = transaction.Merchant.Address.City;


            }


            using (var context = new MonzoEntities())
            {

                context.Transactions.Add(destination);
                await context.SaveChangesAsync();

            }

        }

        public async Task DeleteTransactions()
        {
            using (var context = new MonzoEntities())
            {


                await context.Database.ExecuteSqlCommandAsync("TRUNCATE TABLE [Transactions]");

            }
        }

        public async Task<List<CategorySum>> SumOfEachCategory()
        {

            using (var context = new MonzoEntities())
            {

                List<CategorySum> SumOfEachCategory = await context.Database.SqlQuery<CategorySum>("SumOfEachCategory").ToListAsync();


                return SumOfEachCategory;


            }
        }

        public async Task<List<List<double>>> SumOfEachDay()
        {

            List<List<double>> chartData = new List<List<double>>();

            using (var context = new MonzoEntities())
            {

                var result = await context.Database.SqlQuery<SumOfEachDay>("SumOfEachDay").ToListAsync();

                foreach (var item in result)
                {
                    string resultItem = new String(JsonConvert.SerializeObject(item.Date, new JavaScriptDateTimeConverter()).Where(x => Char.IsDigit(x)).ToArray());

                    chartData.Add(new List<double> { Convert.ToDouble(resultItem), Convert.ToDouble(item.Amount) });
                }

                return chartData;
            }
        }

        public async Task<VisualisationCalculation> GetTransactionInfo()
        {
            List<Transaction> transactions = await GetTransactionList();

            var firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);


            VisualisationCalculation visualisationCalculation = new VisualisationCalculation
            {
                AverageTranscationCost = Math.Round(transactions.Where(x => x.Amount < 0).Sum(x => x.Amount) / transactions.Where(x => x.Amount < 0).Count(), 2) * -1,

                AmmountSpentThisMonth = Math.Round(transactions.Where(x => x.Amount < 0 && x.Created >= firstDayOfMonth && x.Created <= lastDayOfMonth).Sum(x => x.Amount), 2) * -1,

                AmountSpent = Math.Round(transactions.Where(x => x.Amount < 0).Sum(x => x.Amount), 2) * -1


            };

            return visualisationCalculation;
        }

    }
}