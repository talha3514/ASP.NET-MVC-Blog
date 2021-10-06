using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class WriterPanelMessageController : Controller
    {
        MessageManager mm = new MessageManager(new EfMessageDal());
        MessageValidator messageValidator = new MessageValidator();
        public ActionResult Inbox()
        {
            string p = (string)Session["WriterMail"];
            var obj = mm.GetListInbox(p);
            return View(obj);
        }

        public ActionResult Sendbox()
        {
            string p = (string)Session["WriterMail"];
            var obj = mm.GetListSendBox(p);
            return View(obj);
        }

        public ActionResult GetInboxMessageDetails(int id)
        {
            var obj = mm.GetByID(id);
            return View(obj);
        }

        public ActionResult GetSendboxMessageDetails(int id)
        {
            var obj = mm.GetByID(id);
            return View(obj);
        }

        public PartialViewResult MessageListMenu()
        {
            return PartialView();
        }

        [HttpGet]
        public ActionResult NewMessage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewMessage(Message p)
        {
            ValidationResult results = messageValidator.Validate(p);
            if (results.IsValid)
            {
                p.SenderMail = (string)Session["WriterMail"];
                p.MessageDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                mm.MessageAdd(p);
                return RedirectToAction("Inbox");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }
    }
}