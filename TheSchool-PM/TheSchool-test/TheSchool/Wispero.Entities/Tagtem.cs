using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wispero.Entities
{
    public class TagItem
    {
        public int Id { get; set; }
        public string Tag { get; set; }
        public int Count { get; set; }
    }
}
