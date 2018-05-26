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
        public void Create(ProductFormat model, IDbConnection connection)
        {
            connection.Execute("INSERT INTO ProductFormat VALUES ( @ProductID, @Size, @Color, @StockQuantity, @Image)",
                new
                {
                    model.ProductID,
                    model.Size,
                    model.Color,
                    model.StockQuantity,
                    model.Image
                });
        }

        public void Update(ProductFormat model, IDbConnection connection)
        {
            connection.Execute("UPDATE ProductFormat SET Size = @Size, Color = @Color, StockQuantity = @StockQuantity, Image=@Image WHERE ProductFormatID = @ProductFormatID",
                new
                {
                    model.Size,
                    model.Color,
                    model.StockQuantity,
                    model.Image,
                    model.ProductFormatID
                });
        }

        public void Delete(ProductFormat model, IDbConnection connection)
        {
            connection.Execute("DELETE FROM ProductFormat WHERE ProductFormatID = @ProductFormatID",
                new
                {
                    model.ProductFormatID
                });
        }

        public ProductFormat FindById(int ProductFormatID, IDbConnection connection)
        {
            var result = connection.Query<ProductFormat>("SELECT * FROM ProductFormat WHERE ProductFormatID = @ProductFormatID", 
                new
                {
                    ProductFormatID
                });
            ProductFormat productFormat = null;
            foreach (var item in result)
            {
                productFormat = item;
            }
            return productFormat;
        }

        public IEnumerable<ProductFormat> GetAll(IDbConnection connection)
        {
            return connection.Query<ProductFormat>("SELECT * FROM ProductFormat ");
        }
    }
}
