using Artical_Task.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Artical_Task.Controllers
{
    public class AdminController : Controller
    {

        ArticalsDB db= new ArticalsDB();

        //***************************** Add new category*****************************/
        [HttpGet]
        public ActionResult AddCategory()
        {
            Category category = new Category();
            return View(category);
        }

        [HttpPost]
        public ActionResult AddCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Category.Add(category);
                db.SaveChanges();
            }
            return View();
        }


        //*****************************Add new article*****************************/
        [HttpGet]
        public ActionResult AddArticle()
        {
            Article art = new Article();
            art.categories = db.Category.ToList();
            return View(art);
            
        }
        [HttpPost]
        public ActionResult AddArticle(Article art)
        {
            Artical artSaved = new Artical();
            artSaved.text = art.text;
            artSaved.category_id = art.category_id;
            artSaved.dateTime = DateTime.Now;
            artSaved.subject = art.subject;
            db.Artical.Add(artSaved);
            db.SaveChanges();

            Artical_Images img = new Artical_Images();
  
            if(art.files != null)
            {
                foreach(HttpPostedFileBase file in art.files)
                {
                    if (file != null)
                    {
                        string fileName = Path.GetFileName(file.FileName);
                        var ServerSavePath = Path.Combine(Server.MapPath("~/Uploads/") + fileName);
                        file.SaveAs(ServerSavePath);
                        img.art_id = artSaved.id;
                        img.path = fileName;
                        db.Artical_Images.Add(img);
                        db.SaveChanges();
                    }
                }
            }
            return RedirectToAction("GetArts");
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
            foreach (var x in imgs)
            {
                pathes.Add(x.path);
            }
            return pathes;
        }

        [HttpGet]
        public ActionResult EditArt(int id)
        {
            var art = db.Artical.SingleOrDefault(c => c.id == id);
            var art_imgs = GetArtImgs(id);
            var article = new Article()
            {
                id = art.id,
                text = art.text,
                cate_name = art.Category.name,
                pathes = art_imgs
            };

            article.categories = db.Category.ToList();
            return View(article);
        }

        public ActionResult EditArt(Article article)
        {
            var art = db.Artical.SingleOrDefault(c => c.id == article.id);
            art.text = article.text;
            art.category_id = article.category_id;
            db.SaveChanges();
            if (article.files != null)
            {
                foreach (HttpPostedFileBase file in article.files)
                {
                    if (file != null)
                    {
                        string fileName = Path.GetFileName(file.FileName);
                        var ServerSavePath = Path.Combine(Server.MapPath("~/Uploads/") + fileName);
                        file.SaveAs(ServerSavePath);
                        var img = new Artical_Images()
                        {
                            art_id = article.id,
                            path = fileName
                        };
                        db.Artical_Images.Add(img);
                        db.SaveChanges();
                    }
                }
            }
            return RedirectToAction("GetArts");
        }

        public ActionResult DeleteImg(int id , string path)
        {
            var img = db.Artical_Images.SingleOrDefault(c => c.art_id == id && c.path == path);
            db.Artical_Images.Remove(img);
            db.SaveChanges();
            return RedirectToAction("EditArt/" + id);
        }


        public ActionResult DeleteArt(int id)
        {
            var art = db.Artical.SingleOrDefault(c => c.id == id);
            db.Artical.Remove(art);
            db.SaveChanges();
            return RedirectToAction("GetArts");
        }
    }
}