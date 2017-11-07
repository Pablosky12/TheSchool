using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wispero.Entities;
using System.Web.Mvc;
using Wispero.Web.Models;
using Moq;
using System.Linq.Expressions;

namespace Wispero.Web.Test.Controllers
{
    [TestClass]
    public class QuestionControllerTests
    {
        Moq.Mock<Wispero.Core.Services.IDataService<KnowledgeBaseItem>> dataService;
        Moq.Mock<Wispero.Core.Services.IQueryService<KnowledgeBaseItem>> queryService;

        [TestInitialize]
        public void Initialize()
        {
            dataService = new Moq.Mock<Core.Services.IDataService<KnowledgeBaseItem>>();
            dataService.Setup(x => x.Edit(It.IsAny<KnowledgeBaseItem>()));

            queryService = new Moq.Mock<Core.Services.IQueryService<KnowledgeBaseItem>>();
            queryService.Setup(x => x.Get(2)).Returns(new KnowledgeBaseItem { Id = 2, Query = "Question2", Answer = "Answer2", Tags = "Tag2, Tag3", LastUpdateOn = DateTime.Now });
        }

        [TestMethod]
        public void Edit_Loading()
        {
            var controller = new Wispero.Web.Controllers.QuestionController(dataService.Object, queryService.Object);
            var viewResult = controller.Edit(2);

            Assert.IsNotNull(viewResult);
            Assert.IsInstanceOfType(viewResult, typeof(ViewResult));

            var model = ((ViewResult)viewResult).Model as Models.QuesitonAndAnswerEditModel;

            Assert.IsTrue(model.Question == "Question2");
        }

        [TestMethod]
        public void Edit_LoadingNotFound()
        {
            var controller = new Wispero.Web.Controllers.QuestionController(dataService.Object, queryService.Object);
            var viewResult = controller.Edit(1);

            Assert.IsNotNull(viewResult);
            Assert.IsInstanceOfType(viewResult, typeof(RedirectResult));
            Assert.IsTrue(((RedirectResult)viewResult).Url.Contains("Error"));
        }


        [TestMethod]
        public void Edit_UpdatingAttributes()
        {
            var model = new Models.QuesitonAndAnswerEditModel() { Id = 2, Answer = "Answer3", Question = "Question3", Tags = "Tag3" };

            var controller = new Wispero.Web.Controllers.QuestionController(dataService.Object, queryService.Object);
            var viewResult = controller.Edit(model);

            Assert.IsNotNull(viewResult);
            Assert.IsInstanceOfType(viewResult, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void Edit_InvalidUpdate()
        {
            var model = new Models.QuesitonAndAnswerEditModel() { Id = 2, Answer = null, Question = "Question3", Tags = "Tag3" };

            var controller = new Wispero.Web.Controllers.QuestionController(dataService.Object, queryService.Object);
            controller.ModelState.AddModelError("Answer", "Answer field is required");

            var viewResult = controller.Edit(model);

            Assert.IsNotNull(viewResult);
            Assert.IsInstanceOfType(viewResult, typeof(ViewResult));

            var returnedModel = ((ViewResult)viewResult).Model as Models.QuesitonAndAnswerEditModel;

            Assert.AreSame(model, returnedModel);
        }
    }
}
