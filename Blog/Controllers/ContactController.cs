using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class ContactController : Controller
    {
        ContactManager cm = new ContactManager(new EfContactDal());
        ContactValidator cv = new ContactValidator();
        public ActionResult Index()
        {
            var obj = cm.GetList();
            return View(obj);
        }

        public ActionResult GetContactDetails(int id)
        {
            var obj = cm.GetById(id);
            return View(obj);
        }

        public PartialViewResult ContactPartial()
        {
            return PartialView();
        }
    }
}