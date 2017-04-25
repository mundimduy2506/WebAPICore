using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebAPICore.Models
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly WebAPIContext _context;

        public CustomerRepository(WebAPIContext context)
        {
            _context = context;
        }

        public Task<List<Customer>> GetCustomersAsync(int pageSize, int pageNumber, DateTime? frDate)
        {
            IQueryable<Customer> query = _context.Customer;

            if (frDate != null)
            {
                query = query.Where(item => item.EnrolledDate > frDate);
            }
            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            return query.ToListAsync();
        }


        public Task<List<Customer>> GetCustomersAsync()
        {
            return _context.Set<Customer>().ToListAsync();
        }


        public Task AddAsync(Customer cus)
        {
            _context.Set<Customer>().Add(cus);
            return _context.SaveChangesAsync();
        }


        public Task<CustomerReport> GetCustomerByIdAsync(int id)
        { 
            #region Just to test
            //call valued table function (UDF) by FromSql() method, make sure you changed your DbContext
            var temp = _context.Set<CustomerReport>().FromSql("SELECT * FROM dbo.[ufnGetCustomerInfomation](@p0)", id).FirstOrDefaultAsync();
            var x = temp.Result;
            //called valued table function (UDF) by my customized method 
            var rs = _context.DynamicDataFromSql<CustomerReport>("select * from ufnGetCustomerInfomation(@id)",
                                new Dictionary<string, object>() { { "@id", "2" } });
            #endregion
            return _context.Set<CustomerReport>().FromSql("SELECT * FROM dbo.[ufnGetCustomerInfomation](@p0)", id).FirstOrDefaultAsync();
        }


        /// <summary>
        /// This function dynamically calls store procedure/ raw sql query and returns models which is not in entity model
        /// </summary>
        /// <returns>Return sepecified model from store procedures or raw sql</returns>
        public Task<List<CustomerReport>> GetCustomerDynamicAsync()
        {
            var rs = _context.DynamicDataFromSql<CustomerReport>("webapi_GetCustomerDynamicDateFiltered @date",
                                new Dictionary<string, object>() { { "@date", "04/05/2017" } }).ToList();

            return Task.Run(() => rs);
        }

        public bool IsValidDateFormmat(string date)
        {
            try
            {
                DateTime.Parse(date);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
