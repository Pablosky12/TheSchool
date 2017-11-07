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
    public class SingletonPhotoHelper : PhotoHelper
    {
        private static volatile IQueryable<Photo> PhotoList;
        private static object _lock = new object();

        public SingletonPhotoHelper(IRestClient restClient, ISerializer serializer) 
            : base(restClient, serializer)
        {
        }

        public new IQueryable<Photo> GetAll()
        {
            /////////////////////////////////////////////////////////////////////////////////////////////
            //Make your code changes here so we can keep the same reference value object all the time.
            //base.GetAll() should be called once irrespectively of how many instances of SingletonPhotoHelper 
            //are created. You can change LocalCache definition.
            lock (_lock)
            {
                if (PhotoList == null)
                {
                    PhotoList = new PhotoHelper(RestClient, Serializer).GetAll();
                }
                return PhotoList;
            }
            /////////////////////////////////////////////////////////////////////////////////////////////
        }
        
        public new IQueryable<Photo> Match(Expression<Func<Photo, bool>> searchPattern)
        {
            var collection = this.GetAll();
            return collection.Where(searchPattern);
        }   

        public new IQueryable<Photo> Match(Expression<Func<Photo, bool>> searchPattern, Expression<Func<Photo, object>> sorting)
        {
            return Match(searchPattern).OrderBy(sorting);
        }
    }
}
