using BuildSchool.MvcSolution.OnlineStore.Models;
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
            var sql = "UPDATE Orders SET OrderID = @OrderID, ProductFormatID = @ProductFormatID, Quantity = @Quantity, UnitPrice = @UnitPrice WHERE OrderID = @OrderID";

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

        public OrderDetails FindById(int OrderID)
        {
            SqlConnection connection = new SqlConnection(
                "data source=.; database=Commerce; integrated security=true");
            var sql = "SELECT * FROM OrderDetails WHERE OrderID = @OrderID";

            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@OrderID", OrderID);

            connection.Open();

            var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            var orderDetails = new OrderDetails();

            while (reader.Read())
            {
                orderDetails.OrderID = (int)reader.GetValue(reader.GetOrdinal("OderID"));
                orderDetails.ProductFormatID = (int)reader.GetValue(reader.GetOrdinal("ProductFormatID"));
                orderDetails.Quantity = (int)reader.GetValue(reader.GetOrdinal("Quantity"));
                orderDetails.UnitPrice = (decimal)reader.GetValue(reader.GetOrdinal("UnitPrice"));
            }

            reader.Close();

            return orderDetails;
        }

        public IEnumerable<OrderDetails> GetAll()
        {
            SqlConnection connection = new SqlConnection(
                "data source=.; database=Commerce; integrated security=true");
            var sql = "SELECT * FROM OrderDetails";

            SqlCommand command = new SqlCommand(sql, connection);
            connection.Open();

            var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            var orderDetails = new List<OrderDetails>();

            while (reader.Read())
            {
                var orderDetail = new OrderDetails();
                orderDetail.OrderID = (int)reader.GetValue(reader.GetOrdinal("OderID"));
                orderDetail.ProductFormatID = (int)reader.GetValue(reader.GetOrdinal("ProductFormatID"));
                orderDetail.Quantity = (int)reader.GetValue(reader.GetOrdinal("Quantity"));
                orderDetail.UnitPrice = (decimal)reader.GetValue(reader.GetOrdinal("UnitPrice"));

                orderDetails.Add(orderDetail);
            }

            reader.Close();

            return orderDetails;
        }
    }
}
