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
    public class OrdersRepository
    {
        public void Create(Orders model)
        {
            SqlConnection connection = new SqlConnection(
                "data source=.; database=Commerce; integrated security=true");
            var sql = "INSERT INTO Orders VALUES ( @EmployeeID, @MemberID, @ShipName, @ShipAddress, @ShipPhone, @ShippedDate, @OrderDate, @ReceiptedDate, @Discount, @Status)";

            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@EmployeeID", model.EmployeeID);
            command.Parameters.AddWithValue("@MemberID", model.MemberID);
            command.Parameters.AddWithValue("@ShipName", model.ShipName);
            command.Parameters.AddWithValue("@ShipAddress", model.ShipAddress);
            command.Parameters.AddWithValue("@ShipPhone", model.ShipPhone);
            if (model.ShippedDate != null)
            {
                command.Parameters.AddWithValue("@ShippedDate", model.ShippedDate);
            }
            else
            {
                command.Parameters.AddWithValue("@ShippedDate", DBNull.Value);
            }
            command.Parameters.AddWithValue("@OrderDate", model.OrderDate);
            if (model.ReceiptedDate != null)
            {
                command.Parameters.AddWithValue("@ReceiptedDate", model.ReceiptedDate);
            }
            else
            {
                command.Parameters.AddWithValue("@ReceiptedDate", DBNull.Value);
            }
            command.Parameters.AddWithValue("@Discount", model.Discount);
            command.Parameters.AddWithValue("@Status", model.Status);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

        }

        public void Update(Orders model)
        {
            SqlConnection connection = new SqlConnection(
                "data source=.; database=Commerce; integrated security=true");
            var sql = "UPDATE Orders SET EmployeeID = @EmployeeID, MemberID = @MemberID, ShipName = @ShipName, ShipAddress = @ShipAddress, ShipPhone = @ShipPhone, ShippedDate = @ShippedDate, OrderDate=@OrderDate, ReceiptedDate=@ReceiptedDate, Discount=@Discount, Status = @Status WHERE OrderID = @OrderID ";

            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@OrderID", model.OrderID);
            command.Parameters.AddWithValue("@EmployeeID", model.EmployeeID);
            command.Parameters.AddWithValue("@MemberID", model.MemberID);
            command.Parameters.AddWithValue("@ShipName", model.ShipName);
            command.Parameters.AddWithValue("@ShipAddress", model.ShipAddress);
            command.Parameters.AddWithValue("@ShipPhone", model.ShipPhone);
            if (model.ShippedDate != null)
            {
                command.Parameters.AddWithValue("@ShippedDate", model.ShippedDate);
            }
            else
            {
                command.Parameters.AddWithValue("@ShippedDate", DBNull.Value);
            }
            command.Parameters.AddWithValue("@OrderDate", model.OrderDate);
            if (model.ReceiptedDate != null)
            {
                command.Parameters.AddWithValue("@ReceiptedDate", model.ReceiptedDate);
            }
            else
            {
                command.Parameters.AddWithValue("@ReceiptedDate", DBNull.Value);
            }
            command.Parameters.AddWithValue("@Discount", model.Discount);
            command.Parameters.AddWithValue("@Status", model.Status);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

        }
        public void Delete(Orders model)
        {
            SqlConnection connection = new SqlConnection(
                "data source=.; database=Commerce; integrated security=true");
            var sql = "DELETE FROM Orders WHERE OrderID = @OrderID";

            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@OrderID", model.OrderID);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

        }
        public Orders FindById(int OrderID)
        {
            IDbConnection connection = new SqlConnection("data source=.; database=Commerce; integrated security=true");
            var result = connection.Query<Orders>("select * FROM Orders WHERE OrderID = @OrderID", new { OrderID });
            Orders order = null;
            foreach (var item in result)
            {
                order = item;
            }
            return order;
        }
        public IEnumerable<Orders> GetStatus(string Status)
        {
            IDbConnection connection = new SqlConnection("data source=.; database=Commerce; integrated security=true");
            return connection.Query<Orders>("select * FROM Orders WHERE Status = @Status", new { Status });
        }
        public IEnumerable<FindOrderdetaiByOrderID> FindOrderdetaiByOrderID(int orderid)
        {
            IDbConnection connection = new SqlConnection("data source=.; database=Commerce; integrated security=true");
            return connection.Query<FindOrderdetaiByOrderID>("FindOrderdetaiByOrderID", new {  orderid}, commandType: CommandType.StoredProcedure);
        }
        public IEnumerable<Orders> GetOrderDate(string OrderDate)
        {
            IDbConnection connection = new SqlConnection("data source=.; database=Commerce; integrated security=true");
            return connection.Query<Orders>("select * FROM Orders WHERE CONVERT(VARCHAR(25), OrderDate, 126) LIKE @OrderDate", new { OrderDate });
        }
        public IEnumerable<Orders> GetAll()
        {
            IDbConnection connection = new SqlConnection("data source=.; database=Commerce; integrated security=true");
            return connection.Query<Orders>("select * FROM Orders ");
        }
    }
}
