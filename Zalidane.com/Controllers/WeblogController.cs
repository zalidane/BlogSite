using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zalidane.com.Models;
using PagedList;
using System.Net;
using System.Data.Entity.Core;
using System.Text;

namespace ZalidaneSite.Controllers
{
    public class WeblogController : Controller
    {
        #region Private Properties
        
        //private ZalidaneBlogConnection db = new ZalidaneBlogConnection();
        private ZalidaneBlogModel db = new ZalidaneBlogModel();

        // TODO: Don't just return true

        public bool IsAdmin { get { return true; /* return Session["IsAdmin"] != null && (bool)Session["IsAdmin"]; */ } }
        #endregion

        public ViewResult Index(int? page)
        {
            List<Post> Posts = null;
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            var posts = from p in db.Posts select p;

            Posts = posts.OrderByDescending(p => p.DateTime).ToList();

            if (Posts == null)
            {
                Posts = new List<Post>();
                Posts.Add(CreatePost());
            }

            return View(Posts.ToPagedList(pageNumber, pageSize));
        }

        [ValidateInput(false)]
        public ActionResult Update(int? id, string title, string body, DateTime dateTime, string tags)
        {
            if (!IsAdmin)
            {
                RedirectToAction("Index");
            }

            Post post = GetPost(id);
            post.Title = title;
            post.Body = body;
            post.DateTime = dateTime;
            post.Tags.Clear();

            tags = tags ?? string.Empty;
            string[] tagNames = tags.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string tagName in tagNames)
            {
                post.Tags.Add(GetTag(tagName));
            }

            if (!id.HasValue)
            {
                db.Posts.Add(post);
            }
            db.SaveChanges();

            return RedirectToAction("Post", new { id = post.ID });
        }
        
        public ActionResult Edit(int? id)
        {
            Post post = GetPost(id);
            StringBuilder tagList = new StringBuilder();
            foreach (Tag tag in post.Tags)
            {
                tagList.AppendFormat("{0} ", tag.Name);
            }
            ViewBag.Tags = tagList.ToString();

            return View(post);
        }
        
        public ActionResult Post(int? id)
        {
            Post post = null;

            if (id.HasValue)
            {
                post = db.Posts.Where(p => p.ID == id).FirstOrDefault();

                if (post == null)
                {
                    post = CreatePost();
                }
            }

            return View(post);
        }

        #region Helper Methods

        private Tag GetTag(string tagName)
        {
            return db.Tags.Where(tag => tag.Name == tagName).FirstOrDefault() ?? new Tag() { Name = tagName };
        }

        private Post GetPost(int? id)
        {
            return id.HasValue ? db.Posts.Where(post => post.ID == id).First() : new Post() { ID = -1 };
        }

        private Post CreatePost()
        {
            Post post = new Post
            {
                ID = 0,
                Title = "Temporary Post",
                Body = "If you are seeing this text, the database is unavailable.  Please try again later.",
                Comments = new List<Comment>
                 {
                     new Comment
                     { 
                         ID=0, 
                         Body="This is a test comment", 
                         DateTime=DateTime.Today, 
                         Email="admin@zalidane.com", 
                         Name="Zalidane", 
                         PostID=0
                     }
                 },
                DateTime = DateTime.Today,
                Tags = null
            };

            return post;
        }

        #endregion
    }
}