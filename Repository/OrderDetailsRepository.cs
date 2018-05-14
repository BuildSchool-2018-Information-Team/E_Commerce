using BuildSchool.MvcSolution.OnlineStore.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSchool.MvcSolution.OnlineStore.Repository
{
    public class OrderDetailsRepository
    {
        public void Create(OrderDetails model)
        {
            SqlConnection connection = new SqlConnection(
                "data source=.; database=Commerce; integrated security=true");
            var sql = "INSERT INTO OrderDetails VALUES (@OrderID, @ProductFormatID, @Quantity, @UnitPrice) ";

            var request = new ProductFormatRepository();
            var product = request.FindById(model.ProductFormatID);
            if ((product.StockQuantity - model.Quantity) >= 0)
            {
                sql = sql + "UPDATE ProductFormat SET StockQuantity = StockQuantity - @Quantity WHERE ProductFormatID = @ProductFormatID";
                SqlCommand command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@OrderID", model.OrderID);
                command.Parameters.AddWithValue("@ProductFormatID", model.ProductFormatID);
                command.Parameters.AddWithValue("@Quantity", model.Quantity);
                command.Parameters.AddWithValue("@UnitPrice", model.UnitPrice);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            else
            {

            }
        }

        public void Update(OrderDetails model)
        {
            SqlConnection connection = new SqlConnection(
                "data source=.; database=Commerce; integrated security=true");
            var sql = "UPDATE OrderDetails SET OrderID = @OrderID, ProductFormatID = @ProductFormatID, Quantity = @Quantity, UnitPrice = @UnitPrice WHERE OrderID = @OrderID AND ProductFormatID = @ProductFormatID";

            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@OrderID", model.OrderID);
            command.Parameters.AddWithValue("@ProductFormatID", model.ProductFormatID);
            command.Parameters.AddWithValue("@Quantity", model.Quantity);
            command.Parameters.AddWithValue("@UnitPrice", model.UnitPrice);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void Delete(OrderDetails model)
        {
            SqlConnection connection = new SqlConnection(
                "data source=.; database=Commerce; integrated security=true");
            var sql = "DELETE FROM OrderDetails WHERE OrderID = @OrderID";

            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@OrderID", model.OrderID);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
        //更改
        public OrderDetails FindById(int OrderID)
        {
            IDbConnection connection = new SqlConnection("data source=.; database=Commerce; integrated security=true");
            var result = connection.Query<OrderDetails>("SELECT * FROM OrderDetails WHERE OrderID = @OrderID", new { @OrderID = OrderID });
            OrderDetails orderDetail = null;
            foreach(var item in result)
            {
                orderDetail = item;
            }
            return orderDetail;
        }

        public IEnumerable<OrderDetails> GetAll()
        {
            IDbConnection connection = new SqlConnection("data source=.; database=Commerce; integrated security=true");
            return connection.Query<OrderDetails>("SELECT * FROM OrderDetails ");
        }
    }
}
