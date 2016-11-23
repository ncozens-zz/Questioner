using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Questioner.Models;
using System.Web.Mvc;

namespace Questioner.Controllers
{
    public class QuestionsController : ApiController
    {
        private ApplicationsEntities db = new ApplicationsEntities();

        // GET: api/Questions
        public IQueryable<Question> GetQuestions()
        {
            return db.Questions;
        }

        public string GetQuestions(string question)
        {
            Question output = new Question() { Question1 = "Your question already exists, try another" };

            var temp = db.Questions.Where(q => q.Question1 == question); 
            if (temp.Count() == 0)
            {
                output = db.Questions.Add(new Question() { Question1 = question });
                db.SaveChanges();

            }
            return output.Question1;
            
        }

        public int GetQuestions(string question, bool yes)
        {
            var temp = db.Questions.Where(q => q.Question1 == question);
            if (temp.Count() == 0)
            {
                return 0;

            }
            var quest = temp.FirstOrDefault();
            if (yes)
                quest.Yes++;
            else
                quest.No++;
            db.SaveChanges();
            if (yes)
                return quest.Yes;
            else
                return quest.No++;

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool QuestionExists(int id)
        {
            return db.Questions.Count(e => e.id == id) > 0;
        }
    }
}