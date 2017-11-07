using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wispero.Data;
using Wispero.Entities;
using Wispero.Web.Helpers;
using Wispero.Web.Models;

namespace Wispero.Web.Controllers
{
    [NoCache]
    public class HomeController : Controller
    {
        Core.Services.IDataService<KnowledgeBaseItem> KnowledgeBaseData;
        Core.Services.IQueryService<KnowledgeBaseItem> KnowledgeBaseQuery;

        public HomeController(Core.Services.IDataService<KnowledgeBaseItem> dataService, Core.Services.IQueryService<KnowledgeBaseItem> queryService)
        {
            KnowledgeBaseData = dataService;
            KnowledgeBaseQuery = queryService;

            //Implement mapping from QuestionAndAnswerModel to Entities.KnowledgeBaseItem.
            //LastUpdateOn field is set with DateTime.Now and Tags field with lowercase.
            //Also create a map from TagItem to TagModel.
            AutoMapper.Mapper.Initialize(x =>
            {
                x.CreateMap<QuestionAndAnswerModel, Entities.KnowledgeBaseItem>()
                .ForMember(dest => dest.Query, opt => opt.MapFrom(src => src.Question))
                .ForMember(dest => dest.LastUpdateOn, opt => opt.UseValue(DateTime.Now))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags.ToLower()));
               
                x.CreateMap<TagItem, TagModel>();
                
            });
            
        }

        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult Entry()
        {
            //Return partial view "Entry";
            return PartialView("Entry");

        }

        [ChildActionOnly]
        [HttpGet]
        public ActionResult TagCloud()
        {
            //Return partial view "TagCloud" with an instance of TagCloudviewModel.
            //You need to call TagHelper.Process as shown below.
            int tagMaxCount;
            var tagCloud = TagHelper.Process(KnowledgeBaseQuery, out tagMaxCount);

            TagCloudViewModel model = new TagCloudViewModel();
            model.Tags = new List<TagModel>();
            foreach(var tag in tagCloud)
            {
                var mapped = AutoMapper.Mapper.Map<TagItem, TagModel>(tag);
                model.Tags.Add(mapped);
            }
            model.MaxCount = tagMaxCount;
            return PartialView("TagCloud",model);
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New(QuestionAndAnswerModel model)
        {
            // Return partial view "Entry" with an instance of QuestionAndAnswerModel.
            //If model is valid then persists the new entry on DB. Make sure  data changes are committed.
            if (ModelState.IsValid)
            {
                var fromModel = AutoMapper.Mapper.Map<QuestionAndAnswerModel, KnowledgeBaseItem>(model);
                KnowledgeBaseData.Add(fromModel);
                KnowledgeBaseData.CommitChanges();
                model = new QuestionAndAnswerModel();
            }
            
            return PartialView("Entry", model);
        }
    }
}