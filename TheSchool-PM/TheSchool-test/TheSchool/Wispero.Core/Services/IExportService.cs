using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wispero.Core.Services
{
    public interface IExportService<T> where T : Setting
    {
        void Export(List<Entities.KnowledgeBaseItem> source, T settings);
        
    }
}
        