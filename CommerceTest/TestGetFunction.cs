using System;
using System.Linq;
using BuildSchool.MvcSolution.OnlineStore.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommerceTest
{
    [TestClass]
    public class TestGetFunction
    {
        //[TestMethod]
        //public void Test_GetCategoryName()
        //{
        //    var repository = new CategoryRepository();
        //    var category = repository.FindCategoryName("abc");
        //    Assert.IsNull(category);
        //}
        [TestMethod]
        public void Test_FindProductFormatByProductID()
        {
            var repository = new ProductRepository();
            var product = repository.FindProductFormatByProductID(1);
            Assert.IsTrue(product.Count() > 1);
        }
        [TestMethod]
        public void Test_GetHotProduct()
        {
            var repository = new ProductRepository();
            var product = repository.GetHotProduct();
            Assert.IsTrue(product.Count() == 0);
        }
        [TestMethod]
        public void Test_FindProductsByCategory()
        {
            var repository = new CategoryRepository();
            var category = repository.FindProductsByCategory();
            Assert.IsTrue(category.Count() == 0);
        }
        [TestMethod]
        public void Test_GetBuyerOrder()
        {
            var repository = new MemberRepository();
            var member = repository.GetBuyerOrder("123");
            Assert.IsTrue(member.Count() == 0);
        }
        [TestMethod]
        public void Test_FindOrderdetaiByOrderID()
        {
            var repository = new OrdersRepository();
            var order = repository.FindOrderdetaiByOrderID(1);
            Assert.IsTrue(order.Count() == 0);
        }
        [TestMethod]
        public void Test_FindByProductName()
        {
            var repository = new ProductRepository();
            var product = repository.FindByProductName("abc");
            Assert.IsTrue(product.Count() == 0);
        }
        [TestMethod]
        public void Test_FindByName()
        {
            var repository = new EmployeesRepository();
            var product = repository.FindByName("abc");
            Assert.IsNull(product);
        }
        [TestMethod]
        public void Test_GetStatus()
        {
            var repository = new OrdersRepository();
            var orders = repository.GetStatus("出貨中");
            Assert.IsTrue(orders.Count() == 0);
        }
        [TestMethod]
        public void Test_GetHowLongHireDate()
        {
            var repository = new EmployeesRepository();
            var employee = repository.GetHowLongHireDate();
            Assert.IsTrue(employee.Count() >= 0);
        }
        [TestMethod]
        public void Test_GetOrderDate()
        {
            var repository = new OrdersRepository();
            var orders = repository.GetOrderDate("2018%");
            Assert.IsTrue(orders.Count() >= 0);
        }
        [TestMethod]
        public void Test_FindByHireYear()
        {
            var repository = new EmployeesRepository();
            var employee = repository.FindByHireYear(1900, 2000);
            Assert.IsTrue(employee.Count() >= 0);
        }
    }
}
