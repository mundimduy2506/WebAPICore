using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPICore.Response;
using WebAPICore.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPICore.Controllers
{
    [Route("api/v1/portfoliotransaction")]
    public class PortfolioTransactionController : Controller
    {
        private readonly IPortfolioRepository _repo;
        private readonly string[] dTyps = new string[] { "ActualSettlementDate", "ContractualSettlementDate", "EndDate", "EntryDate", "EventCreatedDate", "KnowledgeDate" };
        private readonly string[] rpBasis = new string[] { "", "Daily Investments", "", "", "", "Closed Period Investments" };
        public PortfolioTransactionController(IPortfolioRepository cusRepository)
        {
            _repo = cusRepository;
        }
        // GET: api/v1/portfoliotransaction/abc12345
        [HttpGet]
        [Route("{accountId}/{startDate}/{endDate}/{dateType}/{reportBasis}")]
        public async Task<IActionResult> Get(string accountId, string startDate, string endDate, string dateType, string reportBasis, string pageSize, string pageNumber)
        {
            PortfolioTransaction trans = new PortfolioTransaction();
            try
            {
                var pgSize = String.IsNullOrEmpty(pageSize) ? 5 : Int32.Parse(pageSize);
                var pgNumber = String.IsNullOrEmpty(pageNumber) ? 1 : Int32.Parse(pageNumber);
                //return 400-Bad request due to invalid user parameters
                if (!_repo.IsValidDateFormmat(startDate) || !_repo.IsValidDateFormmat(endDate))
                {
                    return BadRequest("Invalid DateTime format.");
                }
                if (!dTyps.Contains(dateType))
                {
                    return BadRequest("Invalid Date Type.");
                }
                if ("5" != reportBasis && "1" != reportBasis)
                {
                    return BadRequest("Invalid Report Basis");
                }
                trans = await _repo.GetTransactionDetailsAsync(accountId, dateType, rpBasis[Int32.Parse(reportBasis)], startDate, endDate, pgSize, pgNumber);
                
                //To validate parameters
                trans.AccountID = accountId;
                trans.StartDate = DateTime.Parse(startDate);
                trans.EndDate = DateTime.Parse(endDate);
                trans.InquiryBasis = Int32.Parse(reportBasis);
                trans.DateType = dateType;
                
                //to retrieve from AccountAPI
                trans.BookID = "HF";
                trans.OrganizationID = "HOST";
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(trans);
        }
    }
}
