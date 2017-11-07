using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wispero.Entities;
using Wispero.Export.Settings;

namespace Wispero.Export
{
    public class ExportServiceImpl<T> : 
        Core.Services.IExportService<T> where T : Wispero.Core.Setting
    {
        public ExportServiceImpl()
        {

        }

        public void Export(List<KnowledgeBaseItem> source, T settings)
        {
            settings.Export(source);
        }
    }
}
