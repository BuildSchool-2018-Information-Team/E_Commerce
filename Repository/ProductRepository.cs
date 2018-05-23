using BuildSchool.MvcSolution.OnlineStore.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace BuildSchool.MvcSolution.OnlineStore.Repository
{
    public class ProductRepository
    {
        public void Create(Product model)
        {
            SqlConnection connection = new SqlConnection(
                "data source=.; database=Commerce; integrated security=true");
            var sql = "INSERT INTO Products VALUES ( @ProductName, @UnitPrice, @Description, @CategoryID, @ProductImage)";

            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@ProductName", model.ProductName);
            command.Parameters.AddWithValue("@UnitPrice", model.UnitPrice);
            command.Parameters.AddWithValue("@Description", model.Description);
            command.Parameters.AddWithValue("@CategoryID", model.CategoryID);
            command.Parameters.AddWithValue("@ProductImage", model.ProductImage);


            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void Update(Product model)
        {
            SqlConnection connection = new SqlConnection(
                "data source=.; database=Commerce; integrated security=true");
            var sql = "UPDATE Products SET ProductName = @ProductName, UnitPrice = @UnitPrice, Description = @Description, CategoryID = @CategoryID, ProductImage = @ProductImage WHERE ProductID = @ProductID";

            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@ProductID", model.ProductID);
            command.Parameters.AddWithValue("@ProductName", model.ProductName);
            command.Parameters.AddWithValue("@UnitPrice", model.UnitPrice);
            command.Parameters.AddWithValue("@Description", model.Description);
            command.Parameters.AddWithValue("@CategoryID", model.CategoryID);
            command.Parameters.AddWithValue("@ProductImage", model.ProductImage);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void Delete(Product model)
        {
            SqlConnection connection = new SqlConnection(
                "data source=.; database=Commerce; integrated security=true");
            var sql = "DELETE FROM Products WHERE ProductID = @ProductID ";

            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@ProductID", model.ProductID);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public Product FindById(int ProductID)
        {
            IDbConnection connection = new SqlConnection("data source=.; database=Commerce; integrated security=true");
            var result = connection.Query<Product>("SELECT * FROM Products WHERE ProductID = @ProductID", new { ProductID });
            Product product = null;
            foreach (var item in result)
            {
                product = item;
            }
            return product;
        }
        public IEnumerable<GetProductOrder> GetHotProduct()
        {
            SqlConnection connection = new SqlConnection("data source=.; database=Commerce; integrated security=true");
            var affectedRows = connection.Query<GetProductOrder>("GetProductOrder", new {  },commandType: CommandType.StoredProcedure);
            //SqlCommand cmd = new SqlCommand();
            //cmd.Connection = connection;
            //cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //cmd.CommandText = "GetProductOrder";
            ////add any parameters the stored procedure might require
            //connection.Open();
            //var o = cmd.ExecuteNonQuery;
            //connection.Close();
            //return o;
            //SqlConnection cnn = new SqlConnection(cnnString);
            //SqlCommand cmd = new SqlCommand();
            //cmd.Connection = cnn;
            //cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //cmd.CommandText = "ProcedureName";
            //add any parameters the stored procedure might require
            //cnn.Open();
            //object o = cmd.ExecuteScalar();
            //cnn.Close();
            return affectedRows;
        }
        public IEnumerable<Product> FindProductByUnitPrice(decimal lower, decimal upper)
        {
            SqlConnection connection = new SqlConnection("data source=.; database=Commerce; integrated security=true");
            return connection.Query<Product>("FindProductByUnitPrice", new { lower, upper}, commandType: CommandType.StoredProcedure);
        }
        public IEnumerable<FindProductFormatByProductID> FindProductFormatByProductID(int productid)
        {
            SqlConnection connection = new SqlConnection("data source=.; database=Commerce; integrated security=true");
            return connection.Query<FindProductFormatByProductID>("FindProductFormatByProductID", new { productid }, commandType: CommandType.StoredProcedure);
        }
        public IEnumerable<Product> FindByProductName(string ProductName)
        {
            IDbConnection connection = new SqlConnection("data source=.; database=Commerce; integrated security=true");
            return connection.Query<Product>("SELECT * FROM Products WHERE ProductName LIKE @ProductName", new { ProductName });
        }
        public IEnumerable<Product> GetAll()
        {
            IDbConnection connection = new SqlConnection("data source=.; database=Commerce; integrated security=true");
            return connection.Query<Product>("SELECT * FROM Products ORDER BY ProductID DESC ");
        }
    }
}
