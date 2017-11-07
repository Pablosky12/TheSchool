using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wispero.Web.Models
{
    public class ListingViewModel
    {
        public string Tag { get; set; }
        public List<Models.QuestionAndAnswerItemModel> Questions { get; set; }

        public ListingViewModel()
        {
            Questions = new List<QuestionAndAnswerItemModel>();
        }
    }
}