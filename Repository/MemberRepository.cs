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
    public class MemberRepository
    {
        public void Create(Members model, IDbConnection connection)
        {
            connection.Execute("INSERT INTO Members(MemberID, Password, Name, Phone, Address, Email) " +
                "VALUES (@MemberID, @Password, @Name, @Phone, @Address, @Email)",
                new
                {
                    model.MemberID,
                    model.Password,
                    model.Name,
                    model.Phone,
                    model.Address,
                    model.Email
                });
        }

        public void Update(Members model, IDbConnection connection)
        {
            connection.Execute("UPDATE Members SET Password=@Password, Name=@Name, Phone=@Phone, Address=@Address, Email=@Email WHERE MemberID = @MemberID",
                new
                {
                    model.Password,
                    model.Name,
                    model.Phone,
                    model.Address,
                    model.Email,
                    model.MemberID
                });
        }
        public void UpdateGUID(Members model, IDbConnection connection)
        {
            connection.Execute("UPDATE Members SET MemberGUID=@MemberGUID WHERE MemberID = @MemberID",
                new
                {
                    model.MemberGUID,
                    model.MemberID
                });
        }

        public void Delete(Members model, IDbConnection connection)
        {
            connection.Execute("DELETE FROM Members WHERE MemberID = @MemberID",
                new
                {
                    model.MemberID
                });
        }

        public Members FindById(string MemberID, IDbConnection connection)
        {
            var result = connection.Query<Members>("SELECT * FROM Members WHERE MemberID = @MemberID", 
                new
                {
                    MemberID
                });
            Members member = null;
            foreach(var item in result)
            {
                member = item;
            }
            return member;
        }
        public IEnumerable<GetBuyerOrder> GetBuyerOrder(string memberid, IDbConnection connection)
        {
            return connection.Query<GetBuyerOrder>("GetBuyerOrder", 
                new
                {
                    memberid
                }, commandType: CommandType.StoredProcedure);
        }
        public IEnumerable<GetBuyerOrder> GetBuyerOrder(string memberid, SqlConnection connection,IDbTransaction transaction)
        {
            return connection.Query<GetBuyerOrder>("GetBuyerOrder", 
                new
                {
                    memberid
                }, transaction, commandType: CommandType.StoredProcedure);
        }
        public IEnumerable<Members> GetAll(IDbConnection connection)
        {
            return connection.Query<Members>("SELECT * FROM Members");
        }
    }
}
