using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LoanProcessingSystem.Models;

namespace LoanProcessingSystem.Controllers.Admin
{
    public class AfterInspectionController : Controller
    {
        // GET: AfterInspection
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AfterInspection()
        {

            using (MortgageDbEntities dc = new MortgageDbEntities())
            {
                
                var v = dc.LoanForms.Where(a => a.Status == "Inspector Approved");


                var loanForms = dc.LoanForms.Include(l => l.AdminDetail);
                return View(v.ToList());
            }

        }

        [HttpPost]
        public ActionResult ManagerApprove(int ApplicationId)
        {
            using (MortgageDbEntities db = new MortgageDbEntities())
            {
                //updating status in loanForm
                var loanform = db.LoanForms.FirstOrDefault(x => x.ApplicationId == ApplicationId);
                if (loanform == null) throw new Exception("Invalid id: " + ApplicationId);
                loanform.Status = "Approved";
                db.LoanForms.Attach(loanform);
                var entry = db.Entry(loanform);
                entry.Property(e => e.Status).IsModified = true;

                //updating status in StatusTrack
                var status = db.StatusTracks.FirstOrDefault(x => x.ApplicationId == ApplicationId);
                status.Status = "Manager Approved";
                db.StatusTracks.Attach(status);
                var statustrackentry = db.Entry(status);
                statustrackentry.Property(e => e.Status).IsModified = true;

                db.SaveChanges();

                return RedirectToAction("AfterInspection");
            }
        }

        [HttpPost]
        public ActionResult ManagerReject(int ApplicationId)
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
                status.Status = "Manager Rejected";
                db.StatusTracks.Attach(status);
                var statustrackentry = db.Entry(status);
                statustrackentry.Property(e => e.Status).IsModified = true;

                db.SaveChanges();

                return RedirectToAction("AfterInspection");
            }
        }
    }
}