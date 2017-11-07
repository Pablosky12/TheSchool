using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wispero.Entities;
using System.Web.Mvc;
using Wispero.Web.Models;
using Moq;

namespace Wispero.Web.Test.Controllers
{
    [TestClass]
    public class HomeControllerTests
    {
        Moq.Mock<Wispero.Core.Services.IDataService<KnowledgeBaseItem>> dataService;
        Moq.Mock<Wispero.Core.Services.IQueryService<KnowledgeBaseItem>> queryService;

        [TestInitialize]
        public void Initialize()
        {
            dataService = new Moq.Mock<Core.Services.IDataService<KnowledgeBaseItem>>();
            queryService = new Moq.Mock<Core.Services.IQueryService<KnowledgeBaseItem>>();
        }

        [TestMethod]
        public void Index_LoadingPage()
        {
            var controller = new Wispero.Web.Controllers.HomeController(dataService.Object, queryService.Object);
            var viewResult = controller.Index();

            Assert.IsNotNull(viewResult);
            Assert.IsInstanceOfType(viewResult, typeof(ViewResult));
        }

        [TestMethod]
        public void Entry_LoadingPartialView()
        {
            var controller = new Wispero.Web.Controllers.HomeController(dataService.Object, queryService.Object);
            var viewResult = controller.Entry();

            Assert.IsNotNull(viewResult);
            Assert.IsInstanceOfType(viewResult, typeof(PartialViewResult));
        }

        [TestMethod]
        public void TagCloud_LoadingPartialView()
        {
            queryService.Setup(x => x.GetAll()).Returns(
               new System.Collections.Generic.List<KnowledgeBaseItem>() {
                    new KnowledgeBaseItem { Id = 1, Query = "Question1", Answer = "Answer1", Tags = "Tag1, Tag2", LastUpdateOn = DateTime.Now },
                    new KnowledgeBaseItem { Id = 2, Query = "Question2", Answer = "Answer2", Tags = "Tag2, Tag3", LastUpdateOn = DateTime.Now }
               });

            var controller = new Wispero.Web.Controllers.HomeController(dataService.Object, queryService.Object);
            var viewResult = controller.TagCloud();

            Assert.IsNotNull(viewResult);
            Assert.IsInstanceOfType(viewResult, typeof(PartialViewResult));

            var model = ((PartialViewResult)viewResult).Model as Models.TagCloudViewModel;

            Assert.IsTrue(model.MaxCount == 2);
            Assert.IsTrue(model.Tags.Count == 3);
        }

        [TestMethod]
        public void TagCloud_EmptyList()
        {
            queryService.Setup(x => x.GetAll()).Returns(new System.Collections.Generic.List<KnowledgeBaseItem>() { });

            var controller = new Wispero.Web.Controllers.HomeController(dataService.Object, queryService.Object);
            var viewResult = controller.TagCloud();

            Assert.IsNotNull(viewResult);
            Assert.IsInstanceOfType(viewResult, typeof(PartialViewResult));

            var model = ((PartialViewResult)viewResult).Model as Models.TagCloudViewModel;

            Assert.IsTrue(model.MaxCount == 0);
            Assert.IsTrue(model.Tags.Count == 0);
        }

        [TestMethod]
        public void New_ValidQuestion()
        {
            var inputModel = new QuestionAndAnswerModel() { Question = "Question1", Answer = "Answer1", Tags = "Tag1, Tag2" };

            dataService.Setup(x => x.Add(It.IsAny<KnowledgeBaseItem>()));

            var controller = new Wispero.Web.Controllers.HomeController(dataService.Object, queryService.Object);
            var viewResult = controller.New(inputModel);

            Assert.IsNotNull(viewResult);
            Assert.IsInstanceOfType(viewResult, typeof(PartialViewResult));

            var model = ((PartialViewResult)viewResult).Model as Models.QuestionAndAnswerModel;

            Assert.IsTrue(string.IsNullOrEmpty(model.Answer) && string.IsNullOrEmpty(model.Question) && string.IsNullOrEmpty(model.Tags));
        }

        [TestMethod]
        public void New_QuestionIncomplete()
        {
            var inputModel = new QuestionAndAnswerModel() { Question = "Question1", Answer = null, Tags = "Tag1, Tag2" };
            
            dataService.Setup(x => x.Add(It.IsAny<KnowledgeBaseItem>()));
            
            var controller = new Wispero.Web.Controllers.HomeController(dataService.Object, queryService.Object);
            controller.ModelState.AddModelError("Answer", "Answer is required");

            var viewResult = controller.New(inputModel);

            Assert.IsNotNull(viewResult);
            Assert.IsInstanceOfType(viewResult, typeof(PartialViewResult));

            var model = ((PartialViewResult)viewResult).Model as Models.QuestionAndAnswerModel;

            Assert.AreEqual(inputModel, model);
        }
    }
}
