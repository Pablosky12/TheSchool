using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Wispero.Entities;

namespace Wispero.Data
{
    public class TagData : Core.Services.IDataService<TagItem>, Core.Services.IQueryService<TagItem>
    {
        WisperoDbContext _context;

        #region Constructors
        public TagData(WisperoDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Methods

        public void Add(TagItem entity)
        {
            throw new NotImplementedException();
        }

        public void CommitChanges()
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Edit(TagItem entity)
        {
            throw new NotImplementedException();
        }

        public TagItem Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<TagItem> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<TagItem> GetByFilter(Expression<Func<TagItem, bool>> expression)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
