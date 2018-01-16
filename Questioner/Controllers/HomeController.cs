using Questioner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Questioner.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationsEntities db = new ApplicationsEntities();
        public ActionResult Index()
        {
            return View(db.Questions.ToList());
        }

        public ActionResult Question(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }
    }
}