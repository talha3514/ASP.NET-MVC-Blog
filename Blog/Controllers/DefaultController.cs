using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Controllers
{
    [AllowAnonymous]
    public class DefaultController : Controller
    {
        HeadingManeger hm = new HeadingManeger(new EfHeadingDal());
        ContentManager cm = new ContentManager(new EfContentDal());
        public PartialViewResult Index(int id = 0)
        {
            var contentList = cm.GetLisByHeadingId(id);
            return PartialView(contentList);
        }

        public ActionResult Headings()
        {
            var headingList = hm.GetList();
            return View(headingList);
        }
    }
}