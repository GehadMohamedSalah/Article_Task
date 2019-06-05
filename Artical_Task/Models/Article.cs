using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Artical_Task.Models
{
    public class Article
    {
        public int id { get; set; }
        [Required(ErrorMessage = "You must write text for your article...")]
        public string text { get; set; }

        [Required(ErrorMessage = "You must write subject for your article...")]
        [MaxLength(50,ErrorMessage ="Subject can't be more than 50 letters...")]
        public string subject { get; set; }
        [Required(ErrorMessage = "You must assign category to article...")]
        public int category_id { get; set; }
        public string cate_name { get; set; }
        public List<string> pathes { get; set; }
        public List<Comment> art_comments { get; set; }     
        public List<Category> categories { get; set; }
        [Required(ErrorMessage = "Write your comment please...")]
        public string comment { get; set; }
        public int art_id { get; set; }
        public DateTime dateTime { get; set; }
        public HttpPostedFileBase[] files { get; set; }
    }
}