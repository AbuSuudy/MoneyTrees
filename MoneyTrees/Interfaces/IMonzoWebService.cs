using System.Collections.Generic;
using System.Threading.Tasks;
using MoneyTrees.Models;
using RestSharp;

namespace MoneyTrees.Interfaces
{
    public interface IMonzoWebService   
    {

        Task<AccessTokenModel> AuthorisationToken(string code);

        Task<WhoAmIModel> WhoAmI();

        Task LogOut();

        Task<AccountListModel> GetAccounts();

        Task<BalanceModel> GetBalance();


        Task<TransactionListModel> GetTransactionsDB();

        Task GetTransactionsMonzo();

        Task<SingleTransactionItemModel> GetAllTransaction(string transactionId);

        Task<TransactionListModel> GetTransactionsLast90Days();

        Task<AccessTokenModel> RefreshToken();

    }
}