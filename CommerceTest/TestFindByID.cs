using BuildSchool.MvcSolution.OnlineStore.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceTest
{
    [TestClass]
    public class TestFindByID
    {
        IDbConnection connection = new SqlConnection("data source=.; database=Commerce; integrated security=true");
        [TestMethod]
        public void Test_FindByID_Category()
        {
            var repository = new CategoryRepository();
            var category = repository.FindById(1, connection);
            Assert.IsNotNull(category);
        }
        [TestMethod]
        public void Test_FindByID_Product()
        {
            var repository = new ProductRepository();
            var product = repository.FindById(1);
            Assert.IsNotNull(product);
        }
        [TestMethod]
        public void Test_FindByID_Employee()
        {
            var repository = new EmployeesRepository();
            var employee = repository.FindById(1, connection);
            Assert.IsNotNull(employee);
        }
        [TestMethod]
        public void Test_FindByID_Member()
        {
            var repository = new MemberRepository();
            var member = repository.FindById("123", connection);
            Assert.IsNotNull(member);
        }
        [TestMethod]
        public void Test_FindByID_Order()
        {
            var repository = new OrdersRepository();
            var orders = repository.FindById(1);
            Assert.IsNotNull(orders);
        }
        [TestMethod]
        public void Test_FindByID_ProductFormat()
        {
            var repository = new ProductFormatRepository();
            var product = repository.FindById(1);
            Assert.IsNotNull(product);
        }
        [TestMethod]
        public void Test_FindByID_OrderDetails()
        {
            var repository = new OrderDetailsRepository();
            var orderdetails = repository.FindById(1);
            Assert.IsNotNull(orderdetails);
        }
    }
}
