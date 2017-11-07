using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wispero.Entities;

namespace Wispero.Data
{
    public class WisperoDbContext : DbContext
    {
        public WisperoDbContext() : base("DefaultConnection")
        {
        }

        public virtual IDbSet<KnowledgeBaseItem> KnowledgeBaseItems { get; set; }
        public virtual IDbSet<TagItem> TagItems { get; set; }
    }
}
