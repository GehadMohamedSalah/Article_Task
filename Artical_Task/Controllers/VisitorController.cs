using Artical_Task.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Artical_Task.Controllers
{
    public class VisitorController : Controller
    {

        ArticalsDB db = new ArticalsDB();
        // GET: Visitor

        //************************************************Register************************************************************/
        [HttpGet]
        public ActionResult Register()
        {
            User visitor = new User();
            return View(visitor);
        }

        [HttpPost]
        public ActionResult Register(User visitor)
        {
            var user = db.User.SingleOrDefault(c => c.username == visitor.username && c.password == visitor.password);
            if(user == null)
            {
                if (ModelState.IsValid)
                {
                    visitor.job_title = 1;
                    db.User.Add(visitor);
                    db.SaveChanges();
                    return RedirectToAction("GetArts");
                }
            }

            return Content("This user is already exists...");
            
        }

        /*****************************Get articles with comments and filter*****************************/
        [HttpGet]
        public ActionResult GetArts()
        {
            Article art = new Article();
            List<Article> allarts = new List<Article>();
            List<Artical> arts = db.Artical.ToList();
            var cates = new List<Category>();
            for (int x = 0; x < arts.Count; x++)
            {
                art.id = arts[x].id;
                art.subject = arts[x].subject;
                art.text = arts[x].text;
                art.cate_name = arts[x].Category.name;
                art.art_comments = GetArtComments(arts[x].id);
                art.pathes = GetArtImgs(arts[x].id);
                art.dateTime = (DateTime)arts[x].dateTime;
                allarts.Add(art);
                art = new Article();
                cates.Add(arts[x].Category);
            }
            
            var categories = cates.Distinct().ToList();
            ViewBag.categories = categories;
            var allarticles = allarts.OrderByDescending(c => c.dateTime);
            return View(allarticles);
        }

        //*****************************Comment on article*****************************/
        [HttpPost]
        public ActionResult GetArts(Article art)
        {
            int UserID = Convert.ToInt32(Session["UserID"]);
            Comment comm = new Comment()
            {
                text = art.comment,
                art_id = art.art_id,
                user_id = UserID,
                dateTime = DateTime.Now
            };

            db.Comment.Add(comm);
            db.SaveChanges();
            return RedirectToAction("GetArts");
        }

        //*****************************Get article's comments *****************************/
        public List<Comment> GetArtComments(int art_id)
        {
            var comments = new List<Comment>();
            comments = db.Comment.Where(c => c.art_id == art_id).ToList();
            return comments;
        }

        //*****************************Get atricle's images*****************************/
        public List<string> GetArtImgs(int art_id)
        {
            var imgs = new List<Artical_Images>();
            imgs = db.Artical_Images.Where(c => c.art_id == art_id).ToList();
            var pathes = new List<string>();
            foreach(var x in imgs)
            {
                pathes.Add(x.path);
            }
            return pathes;
        }
    }

}
