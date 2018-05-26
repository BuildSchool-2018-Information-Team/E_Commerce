using BuildSchool.MvcSolution.OnlineStore.Models;
using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;
using Dapper;

namespace BuildSchool.MvcSolution.OnlineStore.Repository
{
    public class EmployeesRepository
    {
        public void Create(Employees model, IDbConnection connection)
        {
            connection.Execute("INSERT INTO Employees(Name, Phone, HireDate, Email, Image) VALUES ( @Name, @Phone, @HireDate, @Email, @Image)",
                new
                {
                    model.Name,
                    model.Phone,
                    model.HireDate,
                    model.Email,
                    model.Image
                });
        }

        public void Update(Employees model, IDbConnection connection)
        {
            connection.Execute("UPDATE Employees SET Name=@Name, Phone=@Phone, Email=@Email, Image=@Image WHERE EmployeeID = @EmployeeID",
                new
                {
                    model.Name,
                    model.Phone,
                    model.Email,
                    model.Image,
                    model.EmployeeID
                });
        }
        public void UpdateGUID(Employees model, IDbConnection connection)
        {
            connection.Execute("UPDATE Employees SET EmployeeGUID=@EmployeeGUID WHERE EmployeeID = @EmployeeID",
                new
                {
                    model.EmployeeGUID,
                    model.EmployeeID
                });
        }

        public void Delete(Employees model, IDbConnection connection)
        {
            connection.Execute("DELETE FROM Employees WHERE EmployeeID = @EmployeeID", 
                new
                {
                    model.EmployeeID
                });
        }

        public Employees FindById(int EmployeeID, IDbConnection connection)
        {
            var result = connection.Query<Employees>("SELECT * FROM Employees WHERE EmployeeID = @EmployeeID", 
                new
                {
                    EmployeeID
                });
            Employees employee = null;
            foreach (var item in result)
            {
                employee = item;
            }
            return employee;
        }

        public IEnumerable<Employees> GetAll(IDbConnection connection)
        {
            return connection.Query<Employees>("SELECT * FROM employees");
        }

        public IEnumerable<Employees> FindByHireYear(int startYear, int endYear, IDbConnection connection)
        {
            return connection.Query<Employees>("SELECT * FROM Employees WHERE YEAR(HireDate) BETWEEN @startYear AND @endYear ORDER BY HireDate DESC", 
                new
                {
                    startYear, endYear
                });
        }
        public IEnumerable<GetHowLongHireDate> GetHowLongHireDate(IDbConnection connection)
        {
            return connection.Query<GetHowLongHireDate>("GetHowLongHireDate", 
                new
                {

                }, commandType: CommandType.StoredProcedure);
        }
        public IEnumerable<Employees> FindByName(string Name, IDbConnection connection)
        {
            return connection.Query<Employees>("SELECT * FROM Employees WHERE Name = @Name", 
                new
                {
                    Name
                });
        }
    }
}
