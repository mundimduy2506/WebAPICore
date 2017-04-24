///Author: Duy Tran
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPICore.Models;
using WebAPICore.Response;

namespace WebAPICore.Controllers
{
    [Route("api/customer")]
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _repo;

        public CustomerController(ICustomerRepository cusRepository)
        {
            _repo = cusRepository;
        }


        // GET: api/customer/{frDate}/
        /// <summary>
        /// Retrieves a list of customers having enrolled date after entered date
        /// </summary>
        /// <param name="pageSize">Page size</param>
        /// <param name="pageNumber">Page number</param>
        /// <param name="frDate">From date: mandatory</param>
        /// <returns>List customers</returns>
        [HttpGet]
        [Route("{frDate}")]
        public async Task<IActionResult> Get(string pageSize, string pageNumber, string frDate)
        {
            var response = new ListModelResponse<Customer>() as IListModelResponse<Customer>;
            try
            {
                var pgSize = String.IsNullOrEmpty(pageSize) ? 15 : Int32.Parse(pageSize);
                var pgNumber = String.IsNullOrEmpty(pageNumber) ? 1 : Int32.Parse(pageNumber);
                response.PageNumber = pgNumber;
                response.PageSize = pgSize;

                //return 400-Bad request due to frDate is invalid
                if (_repo.IsValidDateFormmat(frDate))
                {
                    var date = DateTime.Parse(frDate);
                    response.Model = await _repo.GetCustomersAsync(pgSize, pgNumber, date);
                    response.Message = String.Format("Total of {0} records.", response.Model.Count());
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(response.Model);
        }


        // GET: api/customer/id/{id}
        /// <summary>
        /// Retrieves a customer by Id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>customer</returns>
        [HttpGet]
        [Route("id/{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var response = new SingleModelResponse<Customer>() as ISingleModelResponse<Customer>;
            try
            {
                response.Model = await _repo.GetCustomerByIdAsync(Int32.Parse(id));
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(response.Model);
        }


        // GET: api/customer
        /// <summary>
        /// Retrieves all customers
        /// </summary>
        /// <returns>List customers</returns>
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get()
        {
            var response = new ListModelResponse<Customer>() as IListModelResponse<Customer>;
            response.PageNumber = 1;
            response.PageSize = 15;
            response.Model = await _repo.GetCustomersAsync(15, 1, null);
            response.Message = String.Format("Total of {0} records.", response.Model.Count());
            return Ok(response.Model);
        }


        // GET: api/customer/dynamic
        /// <summary>
        /// Retrieves dynamic customers from store procedure which will not bound to entity models
        /// </summary>
        /// <returns>List customers</returns>
        [HttpGet]
        [Route("dynamic/")]
        public async Task<IActionResult> Get(int temp, int temp2)
        {
            var response = new ListModelResponse<CustomerReport>() as IListModelResponse<CustomerReport>;
            response.PageNumber = 1;
            response.PageSize = 15;
            response.Model = await _repo.GetCustomerDynamicAsync();
            response.Message = String.Format("Total of {0} records.", response.Model.Count());
            return Ok(response.Model);
        }


        // POST: api/customer
        /// <summary>
        /// Add new customer to db
        /// </summary>
        /// <param name="cus">Customer obj</param>
        /// <returns>Customer</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Customer cus)
        {
            if (cus == null)
            {
                return BadRequest();
            }

            await _repo.AddAsync(cus);
            return Ok(cus);
        }

    }
}
