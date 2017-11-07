
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Wispero.Web.Binders
{
    public class QnAModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            return BindQnAModel(controllerContext.HttpContext.Request.Form, bindingContext.ModelState);
        }

        public static object BindQnAModel(NameValueCollection values, ModelStateDictionary modelState)
        {

            // Implement model binder for QuestionAndAnswerModel

            var model = new Models.QuestionAndAnswerModel
            {
                Answer = values["txtAnswer"],
                Tags = values["txtTags"],
                Question = values["txtQuestion"],
                                
            };
            if (string.IsNullOrEmpty(model.Answer))  
            {
                modelState.AddModelError("txtAnswer", "You should add an answer");
            }
            else if (string.IsNullOrEmpty(model.Tags))
            {
                modelState.AddModelError("txtTags", "You should add at least one tag");
            }
            else if (string.IsNullOrEmpty(model.Question))
            {
                modelState.AddModelError("Question", "You should add question");
            }
            return model;
            
        }
    }
}