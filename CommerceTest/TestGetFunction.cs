using System;
using BuildSchool.MvcSolution.OnlineStore.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommerceTest
{
    [TestClass]
    public class TestGetFunction
    {
        [TestMethod]
        public void Test_GetCategoryName()
        {
            var repository = new CategoryRepository();
            var category = repository.FindCategoryName("abc");
            Assert.IsNull(category);
        }
        [TestMethod]
        public void Test_FindByProductName()
        {
            var repository = new ProductRepository();
            var product = repository.FindByProductName("abc");
            Assert.IsNull(product);
        }
        [TestMethod]
        public void Test_FindByName()
        {
            var repository = new EmployeesRepository();
            var product = repository.FindByName("abc");
            Assert.IsNull(product);
        }
    }
}
