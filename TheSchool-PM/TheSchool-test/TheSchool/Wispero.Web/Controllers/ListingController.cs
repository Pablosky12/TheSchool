using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wispero.Entities;
using Wispero.Web.Helpers;
using Wispero.Web.Models;

namespace Wispero.Web.Controllers
{
    [NoCache]
    public class ListingController : Controller
    {
        Core.Services.IQueryService<KnowledgeBaseItem> KnowledgeQuery;
        Core.Services.IExportService<Wispero.Export.Settings.QnAMakerSetting> KnowledgeQnAExport;

        public ListingController(Core.Services.IQueryService<KnowledgeBaseItem> queryService, Core.Services.IExportService<Wispero.Export.Settings.QnAMakerSetting> exportService)
        {
            KnowledgeQuery = queryService;
            KnowledgeQnAExport = exportService;

            //Implement mapping from Entities.KnowledgeBaseItem to QuestionAndAnswerItemModel.
            //LastUpdateOn field is set with DateTime.Now and Tags field with lowercase.
            //Also create a map from TagItem to TagModel.
            AutoMapper.Mapper.Initialize(x =>
            {
                x.CreateMap<Entities.KnowledgeBaseItem, QuestionAndAnswerItemModel>()
               .ForMember(dest => dest.Question, opt => opt.MapFrom(src => src.Query))
               .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags.ToLower()));
                x.CreateMap<TagItem, TagModel>();
            });
            
        }

        [HttpGet]
        public ActionResult Index(string tag = "")
        {
            //Implement the corresponding call to get all items or filtered by tag.
            //Return an instance of ListingViewModel.
            List<KnowledgeBaseItem> list;
            ListingViewModel model = new ListingViewModel();

            list = string.IsNullOrEmpty(tag) ? 
                    KnowledgeQuery.GetAll() : KnowledgeQuery.GetByFilter(x => x.Tags.Contains(tag));
           
            foreach (KnowledgeBaseItem item in list)
            {
                QuestionAndAnswerItemModel QnAmodel = AutoMapper.Mapper.Map<KnowledgeBaseItem, QuestionAndAnswerItemModel>(item);
                model.Questions.Add(QnAmodel);
                
            }
            model.Tag = tag;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public FileResult ExportQnAMaker(string fileName, string folder)
        {
            var file = string.IsNullOrEmpty(fileName) ? System.Guid.NewGuid().ToString() + ".txt" : fileName;
            var path = string.IsNullOrEmpty(folder) ? AppDomain.CurrentDomain.BaseDirectory + @"\Export\" : folder;

            //Get all elements and then call the Export method in order to create a text file.
            //Then read and return the file content to the client.

            List<KnowledgeBaseItem> list = KnowledgeQuery.GetAll();
            Export.Settings.QnAMakerSetting export = new Export.Settings.QnAMakerSetting(path, file);
            System.IO.FileInfo exp = new System.IO.FileInfo("file");
            if (!exp.Exists)
            {
                export.Export(list);
            }

            return File("file","application/text");
        }
    }
}