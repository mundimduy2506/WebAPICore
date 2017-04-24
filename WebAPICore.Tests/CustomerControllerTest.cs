using System;
using System.Collections.Generic;
using Moq;
using Xunit;
using System.Threading.Tasks;
using WebAPICore.Models;
using WebAPICore.Controllers;
using WebAPICore.Tests.Ulities;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace WebAPICore.Tests
{
    public class CustomerControllerTest
    {
        private Mock<ICustomerRepository> mock = new Mock<ICustomerRepository>();
        private CustomerController _controller;

        public CustomerControllerTest()
        {
            _controller = new CustomerController(mock.Object);
        }

        [Fact]
        public void Get_Fail_BadRequest()
        {
            // Act  
            var actionResult = _controller.Get("10", "1", "abc") as Task<IActionResult>;
            // Assert  
            Assert.IsType<BadRequestResult>(actionResult.Result);
        }


        [Fact]
        public void Post_Success_OK()
        {
            // Set up Prerequisites   
            var cus = new Customer{ Name = "Peter", EnrolledDate = new DateTime(2017, 04, 01)};

            // Act  
            var actionResult = _controller.Post(cus) as Task<IActionResult>;

            // Assert  
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }


        [Fact]
        public void Post_Fail_BadRequest()
        {
            // Act  
            var actionResult = _controller.Post(null) as Task<IActionResult>;

            // Assert  
            Assert.IsType<BadRequestResult>(actionResult.Result);
        }

        [Fact]
        public void GetCustomersAsync_Success_ReturnsCustomers()
        {
            // Set up Prerequisites  
            var mockedData = GenerateTestCustomers();
            mock
            .Setup(x => x.GetCustomersAsync())
            .ReturnsAsync(mockedData);

            // Act  
            var svc = mock.Object;
            var result = svc.GetCustomersAsync();

            // Assert  
            Assert.True(result.Result.Count==3);
        }

        [Fact]
        public void GetCustomersAsync_Success_ReturnsCustomersAfterFiltering()
        {
            // Set up Prerequisites  
            var mockedData = GenerateTestCustomers();
            mock
            .Setup(x => x.GetCustomersAsync())
            .ReturnsAsync(mockedData);

            // Act
            var svc = mock.Object;
            var result = svc.GetCustomersAsync(10, 1, new DateTime(2018, 4, 1));

            // Assert  
            Assert.Null(result.Result);
        }

        [Theory]
        [InlineData("04/10/2018")]
        [InlineData("04-20-2017")]
        [InlineData("April 19 2017")]
        public void GetCustomersAsync_Success_NoCustomer(string frDate)
        {
            // Set up Prerequisites  
            var mockedData = GenerateTestCustomers();
            mock
            .Setup(x => x.GetCustomersAsync())
            .ReturnsAsync(mockedData);

            // Act
            var svc = mock.Object;
            var result = svc.GetCustomersAsync(10, 1, DateTime.Parse(frDate));

            // Assert  
            Assert.Null(result.Result);
        }
        
        private List<Customer> GenerateTestCustomers()
        {
            var customers = new List<Customer>();
            customers.Add(new Customer()
            {
                EnrolledDate = new DateTime(2017, 4, 10),
                Id = 1,
                Name = "Peter"
            });
            customers.Add(new Customer()
            {
                EnrolledDate = new DateTime(2017, 2, 5),
                Id = 2,
                Name = "Mark"
            });
            customers.Add(new Customer()
            {
                EnrolledDate = new DateTime(2017, 4, 7),
                Id = 3,
                Name = "Lindsay"
            });
            return customers;
        }
    }
}
