using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPICore.Models
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetCustomersAsync();
        Task<List<Customer>> GetCustomersAsync(int pageSize, int pageNumber, DateTime? date);
        Task AddAsync(Customer cus);
        bool IsValidDateFormmat(string date);
        Task<Customer> GetCustomerByIdAsync(int id);
        Task<List<CustomerReport>> GetCustomerDynamicAsync();
    }
}
