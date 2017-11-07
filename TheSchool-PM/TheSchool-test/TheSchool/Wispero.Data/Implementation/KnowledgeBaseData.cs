using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Wispero.Entities;

namespace Wispero.Data
{
    public class KnowledgeBaseData : Core.Services.IDataService<KnowledgeBaseItem>, Core.Services.IQueryService<KnowledgeBaseItem>
    {
        WisperoDbContext _context;

        #region Constructors
        public KnowledgeBaseData(WisperoDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Methods
        public void Add(KnowledgeBaseItem entity)
        {
            _context.KnowledgeBaseItems.Add(entity);
            CommitChanges();
        }

        public void CommitChanges()
        {
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.KnowledgeBaseItems.Remove(this.Get(id));
            CommitChanges();
        }

        public void Edit(KnowledgeBaseItem entity)
        {
            var thisEntity = Get(entity.Id);
           
            if (thisEntity != null)
            {
                thisEntity.Id = entity.Id;
                thisEntity.LastUpdateOn = entity.LastUpdateOn;
                thisEntity.Query = entity.Query;
                thisEntity.Tags = entity.Tags;
                thisEntity.Answer = entity.Answer;
            }
            if (GetByFilter(x => x.RowVersion == entity.RowVersion).Any())
            {
                CommitChanges();
            }
            else
            {
                throw new System.Data.Entity.Infrastructure.DbUpdateConcurrencyException();
            }


        }

        public KnowledgeBaseItem Get(int id)
        {
            return _context.KnowledgeBaseItems.FirstOrDefault(x => x.Id == id);
        }

        public List<KnowledgeBaseItem> GetAll()
        {
            return _context.KnowledgeBaseItems.ToList();
        }

        public List<KnowledgeBaseItem> GetByFilter(Expression<Func<KnowledgeBaseItem, bool>> expression)
        {
            List<KnowledgeBaseItem> retList = new List<KnowledgeBaseItem>();
            var AuxList = this.GetAll();
            foreach (var item in AuxList)
            {
                var currentElem = _context.KnowledgeBaseItems.FirstOrDefault(expression);
                if (AuxList.Contains(currentElem) && !retList.Contains(currentElem))
                {
                    retList.Add(currentElem);
                }
            }


            return retList;
        }

        #endregion
    }
}
