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
        public void Create(Orders model, IDbConnection connection, IDbTransaction transaction)
        {
            connection.Execute("INSERT INTO Orders VALUES ( @EmployeeID, @MemberID, @ShipName, @ShipAddress, @ShipPhone, @ShippedDate, @OrderDate, @ReceiptedDate, @Discount, @Status)", 
                new
                {
                    model.EmployeeID,
                    model.MemberID,
                    model.ShipName,
                    model.ShipAddress,
                    model.ShipPhone,
                    model.ShippedDate,
                    model.OrderDate,
                    model.ReceiptedDate,
                    model.Discount,
                    model.Status
                },transaction);
        }

        public void Update(Orders model, IDbConnection connection)
        {
            connection.Execute("UPDATE Orders SET EmployeeID = @EmployeeID, MemberID = @MemberID, ShipName = @ShipName, ShipAddress = @ShipAddress, ShipPhone = @ShipPhone, ShippedDate = @ShippedDate, OrderDate=@OrderDate, ReceiptedDate=@ReceiptedDate, Discount=@Discount, Status = @Status WHERE OrderID = @OrderID",
                new
                {
                    model.EmployeeID,
                    model.MemberID,
                    model.ShipName,
                    model.ShipAddress,
                    model.ShipPhone,
                    model.ShippedDate,
                    model.OrderDate,
                    model.ReceiptedDate,
                    model.Discount,
                    model.Status,
                    model.OrderID
                });
        }
        public void Delete(Orders model, IDbConnection connection)
        {
            connection.Execute("DELETE FROM Orders WHERE OrderID = @OrderID",
                new
                {
                    model.OrderID
                });
        }
        public Orders FindById(int OrderID, IDbConnection connection)
        {
            var result = connection.Query<Orders>("select * FROM Orders WHERE OrderID = @OrderID", 
                new
                {
                    OrderID
                });
            Orders order = null;
            foreach (var item in result)
            {
                order = item;
            }
            return order;
        }
        public IEnumerable<Orders> GetStatus(string Status, IDbConnection connection)
        {
            return connection.Query<Orders>("select * FROM Orders WHERE Status = @Status", 
                new
                {
                    Status
                });
        }
        public IEnumerable<FindOrderdetaiByOrderID> FindOrderdetaiByOrderID(int orderid, IDbConnection connection)
        {
            return connection.Query<FindOrderdetaiByOrderID>("FindOrderdetaiByOrderID", 
                new
                {
                    orderid
                }, commandType: CommandType.StoredProcedure);
        }
        public IEnumerable<Orders> GetOrderDate(string OrderDate, IDbConnection connection)
        {
            return connection.Query<Orders>("select * FROM Orders WHERE CONVERT(VARCHAR(25), OrderDate, 126) LIKE @OrderDate", 
                new
                {
                    OrderDate
                });
        }
        public IEnumerable<Orders> GetAll(IDbConnection connection)
        {
            return connection.Query<Orders>("select * FROM Orders ");
        }
    }
}
