using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wispero.Entities;
using System.Web.Mvc;
using Wispero.Web.Models;
using Moq;

namespace Wispero.Web.Test.Binders
{
    [TestClass]
    public class QnAModelBinderTest
    {
        [TestMethod]
        public void SendingValidRequest()
        {
            var form = new System.Collections.Specialized.NameValueCollection();
            form.Add("txtQuestion", "Question9");
            form.Add("txtTags", "Tag9");
            form.Add("txtAnswer", "Answer9");

            var modelState = new System.Web.Mvc.ModelStateDictionary();

            var response = Wispero.Web.Binders.QnAModelBinder.BindQnAModel(form, modelState);

            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(QuestionAndAnswerModel));
            Assert.IsTrue(modelState.Count == 0);
        }

        [TestMethod]
        public void SendingMissingValueRequest()
        {
            var form = new System.Collections.Specialized.NameValueCollection();
            form.Add("txtTags", "Tag9");
            form.Add("txtAnswer", "Answer9");

            var modelState = new System.Web.Mvc.ModelStateDictionary();

            var response = Wispero.Web.Binders.QnAModelBinder.BindQnAModel(form, modelState);

            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(QuestionAndAnswerModel));
            Assert.IsTrue(modelState.Count == 1);
            Assert.IsTrue(modelState["Question"].Errors.Count > 0);
        }
    }
}
