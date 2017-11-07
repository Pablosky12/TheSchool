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
    public class ListingControllerTests
    {
        Moq.Mock<Wispero.Core.Services.IQueryService<KnowledgeBaseItem>> queryService;

        [TestInitialize]
        public void Initialize()
        {
            queryService = new Moq.Mock<Core.Services.IQueryService<KnowledgeBaseItem>>();

            queryService.Setup(x => x.GetAll()).Returns(
                new System.Collections.Generic.List<KnowledgeBaseItem>() {
                    new KnowledgeBaseItem { Id = 1, Query = "Question1", Answer = "Answer1", Tags = "Tag1, Tag2", LastUpdateOn = DateTime.Now },
                    new KnowledgeBaseItem { Id = 2, Query = "Question2", Answer = "Answer2", Tags = "Tag2, Tag3", LastUpdateOn = DateTime.Now }
                });

            queryService.Setup(x => x.GetByFilter(Moq.It.IsAny<Expression<Func<KnowledgeBaseItem, bool>>>())).Returns(
               new System.Collections.Generic.List<KnowledgeBaseItem>() {
                    new KnowledgeBaseItem { Id = 1, Query = "Question1", Answer = "Answer1", Tags = "Tag1, Tag2", LastUpdateOn = DateTime.Now },
               });
        }

        [TestMethod]
        public void Index_LoadingPageWithEmptyTag()
        {
            var controller = new Wispero.Web.Controllers.ListingController(queryService.Object, null);
            var viewResult = controller.Index();

            Assert.IsNotNull(viewResult);
            Assert.IsInstanceOfType(viewResult, typeof(ViewResult));

            var model = ((ViewResult)viewResult).Model as Models.ListingViewModel;

            Assert.IsTrue(string.IsNullOrEmpty(model.Tag));
            Assert.IsTrue(model.Questions.Count == 2);
        }

        [TestMethod]
        public void Index_LoadingPageWithTag()
        {
            var controller = new Wispero.Web.Controllers.ListingController(queryService.Object, null);
            var viewResult = controller.Index(tag: "Tag1");

            Assert.IsNotNull(viewResult);
            Assert.IsInstanceOfType(viewResult, typeof(ViewResult));

            var model = ((ViewResult)viewResult).Model as Models.ListingViewModel;

            Assert.IsFalse(string.IsNullOrEmpty(model.Tag));
            Assert.IsTrue(model.Questions.Count == 1);
        }

        [TestMethod]
        public void Export_DownloadQnA()
        {
            Core.Services.IExportService<Export.Settings.QnAMakerSetting> exportService = new Export.ExportServiceImpl<Export.Settings.QnAMakerSetting>();
            var controller = new Wispero.Web.Controllers.ListingController(queryService.Object, exportService);
            var result = controller.ExportQnAMaker(string.Empty, AppDomain.CurrentDomain.BaseDirectory);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(FileResult));

            var fileResult = result as FileResult;

            Assert.IsTrue(fileResult.ContentType == "application/text");
        }
    }
}
