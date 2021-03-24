using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using mail2.Models;
using System.Net.Mail;

namespace mail2.Controllers
{
    public class SendMailerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SendMailer
        public ActionResult Index()
        {
            System.Diagnostics.Debug.WriteLine("This is a log");
            // return View(db.MailModels.ToList());
            return View();
        }

        [HttpPost]
        public ViewResult Index(mail2.Models.MailModel _objModelMail)
        {
            if (ModelState.IsValid)
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(_objModelMail.To);
                mail.From = new MailAddress(_objModelMail.From);
                mail.Subject = _objModelMail.Subject;
                string Body = _objModelMail.Body;
                mail.Body = Body;
                mail.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential
               ("tuinfomedia@gmail.com", "skolan123");// Skriv in anvnamn och lösenord
                smtp.EnableSsl = true;
                smtp.Send(mail);

                return View("Index", _objModelMail);
              
            }
            else
            {
                return View();
            }
        }












        // GET: SendMailer/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MailModel mailModel = db.MailModels.Find(id);
            if (mailModel == null)
            {
                return HttpNotFound();
            }
            return View(mailModel);
        }

        // GET: SendMailer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SendMailer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "From,To,Subject,Body")] MailModel mailModel)
        {
            if (ModelState.IsValid)
            {
                db.MailModels.Add(mailModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mailModel);
        }

        // GET: SendMailer/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MailModel mailModel = db.MailModels.Find(id);
            if (mailModel == null)
            {
                return HttpNotFound();
            }
            return View(mailModel);
        }

        // POST: SendMailer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "From,To,Subject,Body")] MailModel mailModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mailModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mailModel);
        }

        // GET: SendMailer/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MailModel mailModel = db.MailModels.Find(id);
            if (mailModel == null)
            {
                return HttpNotFound();
            }
            return View(mailModel);
        }

        // POST: SendMailer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            MailModel mailModel = db.MailModels.Find(id);
            db.MailModels.Remove(mailModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
