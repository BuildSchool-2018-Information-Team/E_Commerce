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
        public void Test_CategoryName()
        {
            var repository = new ProductRepository();
            var product = repository.FindByProductName("abc");
            Assert.IsNull(product);
        }
    }
}
