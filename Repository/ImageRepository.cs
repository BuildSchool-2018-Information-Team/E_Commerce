using BuildSchool.MvcSolution.OnlineStore.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSchool.MvcSolution.OnlineStore.Repository
{
    public class ImageRepository
    {
        public void Create(Images model, IDbConnection connection)
        {
            connection.Execute("INSERT INTO Image VALUES ( @Image )",
                new
                {
                    model.Image
                });
        }


        public void Update(Images model, IDbConnection connection)
        {
            connection.Execute("UPDATE Image SET Image=@Image WHERE ImageID = @ImageID",
                new
                {
                    model.Image,
                    model.ImageID
                });
        }

        public void Delete(Images model, IDbConnection connection)
        {
            connection.Execute("DELETE FROM Image WHERE ImageID = @ImageID",
                new
                {
                    model.ImageID
                });
        }

        public Images FindById(int ImageID, IDbConnection connection)
        {
            var result = connection.Query<Images>("SELECT * FROM Image WHERE ImageID = @ImageID",
                new
                {
                    ImageID
                });
            Images images = null;
            foreach (var item in result)
            {
                images = item;
            }
            return images;
        }
        public IEnumerable<Images> GetAll(IDbConnection connection)
        {
            return connection.Query<Images>("SELECT * FROM Image");
        }
    }
}
