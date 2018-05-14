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
    public class ProductFormatRepository
    {
        public void Create(ProductFormat model)
        {
            SqlConnection connection = new SqlConnection(
                "data source=.; database=Commerce; integrated security=true");
            var sql = "INSERT INTO ProductFormat VALUES ( @ProductID, @Size, @Color, @StockQuantity)";

            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@ProductID", model.ProductID);
            command.Parameters.AddWithValue("@Size", model.Size);
            command.Parameters.AddWithValue("@Color", model.Color);
            command.Parameters.AddWithValue("@StockQuantity", model.StockQuantity);


            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void Update(ProductFormat model)
        {
            SqlConnection connection = new SqlConnection(
                "data source=.; database=Commerce; integrated security=true");
            var sql = "UPDATE ProductFormat SET Size = @Size, Color = @Color, StockQuantity = @StockQuantity WHERE ProductFormatID = @ProductFormatID";

            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@ProductFormatID", model.ProductFormatID);
            command.Parameters.AddWithValue("@ProductID", model.ProductID);
            command.Parameters.AddWithValue("@Size", model.Size);
            command.Parameters.AddWithValue("@Color", model.Color);
            command.Parameters.AddWithValue("@StockQuantity", model.StockQuantity);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void Delete(ProductFormat model)
        {
            SqlConnection connection = new SqlConnection(
                "data source=.; database=Commerce; integrated security=true");
            var sql = "DELETE FROM ProductFormat WHERE ProductFormatID = @ProductFormatID ";

            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@ProductFormatID", model.ProductFormatID);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public ProductFormat FindById(int ProductFormatID)
        {
            IDbConnection connection = new SqlConnection("data source=.; database=Commerce; integrated security=true");
            var result = connection.Query<ProductFormat>("SELECT * FROM ProductFormat WHERE ProductFormatID = @ProductFormatID", new { @ProductFormatID = ProductFormatID });
            ProductFormat productFormat = null;
            foreach(var item in result)
            {
                productFormat = item;
            }
            return productFormat;
        }

        public IEnumerable<ProductFormat> GetAll()
        {
            IDbConnection connection = new SqlConnection("data source=.; database=Commerce; integrated security=true");
            return connection.Query<ProductFormat>("SELECT * FROM ProductFormat ");
        }
    }
}
