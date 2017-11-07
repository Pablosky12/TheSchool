using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ImageSource.Helper
{
    public class PhotoHelper
    {
        public IRestClient RestClient;
        public ISerializer Serializer;

        public PhotoHelper(IRestClient restClient, ISerializer serializer)
        {
            RestClient = restClient;
            Serializer = serializer;
        }

        public IQueryable<Photo> GetAll()
        {
            var rawContent = RestClient.Get("https://jsonplaceholder.typicode.com/photos");
            var collection = Serializer.ToObject<IList<Photo>>(rawContent);

            return collection.AsQueryable();
        }
        
        public IQueryable<Photo> Match(Expression<Func<Photo, bool>> searchPattern)
        {
            var collection = this.GetAll();
            return collection.Where(searchPattern);
        }

        public IQueryable<Photo> Match(Expression<Func<Photo, bool>> searchPattern, Expression<Func<Photo, object>> sorting)
        {
            return Match(searchPattern).OrderBy(sorting);
        }
    }
}
