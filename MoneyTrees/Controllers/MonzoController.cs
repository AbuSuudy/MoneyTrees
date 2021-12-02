
using MoneyTrees.Models;
using MoneyTrees.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Linq;
using MoneyTrees.Interfaces;

namespace MoneyTrees.Controllers
{
    public class MonzoController : Controller
    {
        protected IMonzoWebService monzoWebService;
        protected IMonzoMapper mapper;
        public MonzoController(IMonzoWebService monzoWebService, IMonzoMapper mapper)
         {
            this.monzoWebService = monzoWebService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetTransactions()
        {

            TransactionListModel transaction = await monzoWebService.GetTransactionsDB();


            List<TrasactionViewModel> destination = mapper.Mapper().Map<List<TransactionModel>, List<TrasactionViewModel>>(transaction.transactions).OrderBy(x => x.Created).ToList();

            return Json(new { data = destination }, JsonRequestBehavior.AllowGet);

        }


    }
}