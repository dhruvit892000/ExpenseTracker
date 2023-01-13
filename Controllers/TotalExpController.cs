using Exptracker2.Data;
using Exptracker2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Exptracker2.Controllers
{
    public class TotalExpController : Controller
    {
        AppDbContext db;
        public TotalExpController(AppDbContext _db)
        {
            this.db = _db;
        }

        public ActionResult TotallimitView()
        {
            var totallimits = db.TotalExplims.Select(tl => tl);
            return View(totallimits);
        }
        public ActionResult Totallimitform()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Totallimitform(TotalExplim texp)
        {
            var data = db.TotalExplims.ToList();
            int countRow = data.Count();
            if (countRow == 0)
            {

                db.TotalExplims.Add(texp);

                db.SaveChanges();
                TempData["ResultOk"] = "New Expense Limit Added Successfully !";
                return RedirectToAction("Dashboard", "Expense");

            }
            else
            {

                TempData["AlertMsg"] = "Total Expense Limit Already Inputted";
                return View();
            }
      
        }

        public ActionResult TotallimitEdit(int id)
        {

            return View(db.TotalExplims.Where(c => c.ExpLimit_Id == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult TotallimitEdit(int id, TotalExplim texp)
        {
            try
            {
                db.Entry(texp).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Alert editlimit"] = "New Expenselimit Updated Successfully!";
                return RedirectToAction("Dashboard","Expense");
            }
            catch
            {
                return View();

            }
        }

        public ActionResult TotallimiDelete(int id)
        {
            TotalExplim texpl = db.TotalExplims.FirstOrDefault(c => c.ExpLimit_Id == id);
            if (texpl != null)
            {
                db.Remove(texpl);
                db.SaveChanges();
                TempData["Alert deletelimit"] = " Expenselimit Deleted Successfully!";
                return RedirectToAction("TotallimitView");
            }
            return View();
        }
    }
}
