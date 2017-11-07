using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wispero.Entities
{
    public class KnowledgeBaseItem
    {
        public int Id { get; set; }
        public string Query { get; set; }
        public string Answer { get; set; }
        public string Tags { get; set; }
        public DateTime LastUpdateOn { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
