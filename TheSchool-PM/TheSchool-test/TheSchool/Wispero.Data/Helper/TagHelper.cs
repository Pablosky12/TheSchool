using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wispero.Data
{
    public static class TagHelper
    {
        public static List<Entities.TagItem> Process(Core.Services.IQueryService<Entities.KnowledgeBaseItem> knowledgeService, out int tagMaxCount)
        {
            var sourceItems = knowledgeService.GetAll();
            return Process(sourceItems, out tagMaxCount);
        }

        public static List<Entities.TagItem> Process(List<Entities.KnowledgeBaseItem> items, out int tagMaxCount)
        {
            tagMaxCount = items.Any() ? 1 : 0;
            List<Entities.TagItem> retList = new List<Entities.TagItem>();
            //Based on the list of items you need to rank tags dynamically. Also, return the max value that will be use for Tag Could control.
            foreach (Entities.KnowledgeBaseItem item in items)
            {
                string[] tagArray;
                tagArray = item.Tags.Split(',');
                
                int tagid = 0;
                foreach (string tag in tagArray)
                {
                    if (!retList.Exists(x => x.Tag == tag.Trim()))
                    {
                        retList.Add(
                            new Entities.TagItem
                            {
                                Tag = tag.Trim(),
                                Id = tagid,
                                Count = 1
                            }
                        );
                    }
                    else
                    {
                        var counter = retList.FirstOrDefault(x => x.Tag == tag.Trim()).Count + 1;
                        retList.FirstOrDefault(x => x.Tag == tag.Trim()).Count = counter;
                        tagMaxCount = tagMaxCount > counter ? tagMaxCount : counter;
                    }

                }
            }

            return retList;
        }
    }
}
