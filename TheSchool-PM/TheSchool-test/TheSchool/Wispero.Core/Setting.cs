using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wispero.Entities;

namespace Wispero.Core
{
    public abstract class Setting
    {
        public string Target { get; private set; }

        public abstract void Export(List<KnowledgeBaseItem> source);
        
        public Setting(string target)
        {
            Target = target;
        }
    }
}
