using MoneyTrees.DAL;
using MoneyTrees.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyTrees.Interfaces
{
    public interface IMonzoRepository 
    {
        Task DeleteTransactions();


        Task InsertTransaction(TransactionModel transaction);

        Task<List<CategorySum>> SumOfEachCategory();


        Task<List<List<double>>> SumOfEachDay();


        Task InsertTokens(AccessTokenModel token);

        Task<AccessTokenModel> GetToken();


        Task<List<Transaction>> GetTransactionList();

        Task<VisualisationCalculation> GetTransactionInfo();



    }
}