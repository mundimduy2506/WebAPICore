using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using WebAPICore.Models;
using WebAPICore.Controllers;
using Microsoft.Extensions.DependencyInjection;

namespace WebAPICore.TestInMemory
{
    [TestClass]
    public class UnitTest1
    {
        private DbContextOptions<WebAPIContext> options = new DbContextOptionsBuilder<WebAPIContext>()
                .UseInMemoryDatabase(databaseName: "WebAPI")
                .Options;

        private static DbContextOptions<WebAPIContext> CreateNewContextOptions()
        {
            // Create a fresh service provider, and therefore a fresh 
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<WebAPIContext>();
            builder.UseInMemoryDatabase()
                   .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }

        [TestMethod]
        public void Add_customers_to_database()
        {
            
            // Run the test against one instance of the context
            using (var context = new WebAPIContext(options))
            {
                var service = new CustomerRepository(context);
                service.AddAsync(new Customer { Name = "Aida", EnrolledDate = new DateTime(2017, 4, 20) });
                service.AddAsync(new Customer { Name = "Duy", EnrolledDate = new DateTime(2017, 4, 22) });
                Assert.AreEqual(2, context.Customer.CountAsync().Result);
                Assert.AreEqual("Aida", context.Customer.SingleAsync(t => t.Id == 1).Result.Name);
            }
        }



        [TestMethod]
        public void Get_All_Customers_from_database()
        {
            
            // Add 1 more customer
            using (var context = new WebAPIContext(options))
            {
                var service = new CustomerRepository(context);
                service.AddAsync(new Customer { Name = "Lindsay", EnrolledDate = new DateTime(2017, 4, 19) });
                var result = service.GetCustomersAsync();
                Assert.AreEqual(3, result.Result.Count);
            }
        }


        [TestMethod]
        public void Get_Customers_filtered_from_database()
        {
            
            using (var context = new WebAPIContext(options))
            {
                var service = new CustomerRepository(context);
                var result = service.GetCustomersAsync(10, 1, new DateTime(2017, 4, 19));
                Assert.AreEqual(2, result.Result.Count);
            }
        }
    }
}
