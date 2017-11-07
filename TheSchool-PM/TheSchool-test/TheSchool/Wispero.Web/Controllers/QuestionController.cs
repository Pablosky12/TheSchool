using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wispero.Web.Helpers;
using Wispero.Web.Models;

namespace Wispero.Web.Controllers
{
    [NoCache]
    public class QuestionController : Controller
    {
        Core.Services.IDataService<Entities.KnowledgeBaseItem> KnowledgeData;
        Core.Services.IQueryService<Entities.KnowledgeBaseItem> KnowledgeQuery;

        public QuestionController(Core.Services.IDataService<Entities.KnowledgeBaseItem> knowledgeData, Core.Services.IQueryService<Entities.KnowledgeBaseItem> knowledgeQuery)
        {
            KnowledgeData = knowledgeData;
            KnowledgeQuery = knowledgeQuery;

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Entities.KnowledgeBaseItem, QuesitonAndAnswerEditModel>()
                    .ForMember(
                            dest => dest.Question, opt => opt.MapFrom(src => src.Query)
                        );
                cfg.CreateMap<QuesitonAndAnswerEditModel, Entities.KnowledgeBaseItem>()
                    .ForMember(
                            dest => dest.Query, opt => opt.MapFrom(src => src.Question)
                        );
            });


        }
        // GET: Question
        public ActionResult Edit(int id)
        {
            var retEntity = AutoMapper.Mapper.Map<Entities.KnowledgeBaseItem, QuesitonAndAnswerEditModel>(KnowledgeQuery.Get(id));
            if (retEntity != null)
            {
                return View(retEntity);
            }
            else return Redirect("Error");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(QuesitonAndAnswerEditModel model)
        {
            if (ModelState.IsValid)
            {

                var entity = AutoMapper.Mapper.Map<Entities.KnowledgeBaseItem>(model);
                try
                {
                    //Implement this part of code to persist changes into database.
                    KnowledgeData.Edit(entity);
                    return RedirectToRoute("Home");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("Concurrency", "Another user might have modified the same record you are trying to make updates. Please refresh and try again.");
                }
            }
            return View(model);
        }
    }
}