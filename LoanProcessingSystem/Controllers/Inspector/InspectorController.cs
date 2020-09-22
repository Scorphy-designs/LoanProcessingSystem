﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LoanProcessingSystem.Models;


namespace LoanProcessingSystem.Controllers
{
    public class InspectorController : Controller
    {
        private MortgageDbEntities db = new MortgageDbEntities();
        // GET: Inspector
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult InspectorPanel()
        {
            return View();
        }

        public ActionResult InspectorView()
        {

            using (MortgageDbEntities dc = new MortgageDbEntities())
            {
                int userid = int.Parse(Session["id"].ToString());
                var v = dc.LoanForms.Where(a => a.InspectorId == userid && a.Status == "Inspector Assigned");


                var loanForms = db.LoanForms.Include(l => l.AdminDetail);
                return View(v.ToList());
            }


        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoanForm loanForm = db.LoanForms.Find(id);
            if (loanForm == null)
            {
                return HttpNotFound();
            }
            return View(loanForm);
        }


  

        [HttpPost]
        public ActionResult ApproveForm(int ApplicationId)
        {
            using (MortgageDbEntities db = new MortgageDbEntities())
            {
                //updating status in loanForm
                var loanform = db.LoanForms.FirstOrDefault(x => x.ApplicationId == ApplicationId);
                if (loanform == null) throw new Exception("Invalid id: " + ApplicationId);
                loanform.Status = "Inspector Approved";
                db.LoanForms.Attach(loanform);
                var entry = db.Entry(loanform);
                entry.Property(e => e.Status).IsModified = true;

                //updating status in StatusTrack
                var status = db.StatusTracks.FirstOrDefault(x => x.ApplicationId == ApplicationId);
                status.Status = "Inspector Approved";
                db.StatusTracks.Attach(status);
                var statustrackentry = db.Entry(status);
                statustrackentry.Property(e => e.Status).IsModified = true;

                db.SaveChanges();

                return RedirectToAction("InspectorView");
            }
        }

        [HttpPost]
        public ActionResult RejectForm(int ApplicationId)
        {
            using (MortgageDbEntities db = new MortgageDbEntities())
            {
                //updating status in loanForm
                var loanform = db.LoanForms.FirstOrDefault(x => x.ApplicationId == ApplicationId);
                if (loanform == null) throw new Exception("Invalid id: " + ApplicationId);
                loanform.Status = "Rejected";
                db.LoanForms.Attach(loanform);
                var entry = db.Entry(loanform);
                entry.Property(e => e.Status).IsModified = true;

                //updating status in StatusTrack
                var status = db.StatusTracks.FirstOrDefault(x => x.ApplicationId == ApplicationId);
                status.Status = "Inspector Rejected";
                db.StatusTracks.Attach(status);
                var statustrackentry = db.Entry(status);
                statustrackentry.Property(e => e.Status).IsModified = true;

                db.SaveChanges();

                return RedirectToAction("InspectorView");
            }
        }



        public ActionResult Logout()
        {
            string Id = (string)Session["Id"];
            Session.Abandon();
            return RedirectToAction("LogIn", "RegisterLogin");
        }

    }
}