using Exptracker2.Data;
using Exptracker2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Exptracker2.Controllers
{
    public class CategoryController : Controller
    {
        AppDbContext db;
        public CategoryController(AppDbContext _db)
        {
            this.db = _db;
        }
        public ActionResult CategoryView()
        {
            IEnumerable<Category> categories = db.Categories.Select(c => c).ToList();

            return View(categories);
        }
        public ActionResult Create()
        {
            if(db.TotalExplims.IsNullOrEmpty())
            {
                TempData["alert"] = "Please Add Total Expense Limit!";
                return Redirect("/Expense/Dashboard");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Create(Category catobj )
        {
           
            var total = db.TotalExplims.Take(1).FirstOrDefault();
            var totalExpenseLimit = total.Expense_Limit_Amt;
            var categorysum = db.Categories.Select(x => x.Catexplimit).Sum();

            var totalCategorysum = categorysum + catobj.Catexplimit;

            if (ModelState.IsValid)
            {
              

                if (totalCategorysum > total.Expense_Limit_Amt)
                {

                    TempData["ResultOk"] = "Category Limit should not be greater than Total Exp Limit";
                    return View();
                }
                else
                {

                    db.Categories.Add(catobj);
                    db.SaveChanges();
                    TempData["Alert addcat"] = "New Category Added Successfully!";
                    return RedirectToAction("Dashboard", "Expense");
                }

            }

            return View(catobj);

        }





        public ActionResult Edit(int id)
        {
            if (db.TotalExplims.IsNullOrEmpty())
            {
                TempData["alertedit"] = "Please Add Total Expense Limit!";
                return Redirect("/Expense/Dashboard");
            }
          
            return View(db.Categories.Where(c => c.Cat_Id == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(int id,  Category catobj)
        {
            var total = db.TotalExplims.Take(1).FirstOrDefault();
            var totalExpenseLimit = total.Expense_Limit_Amt;
            var categorysum = db.Categories.Select(x => x.Catexplimit).Sum();

            var rslt_Pre_C_Amt = db.Categories.FirstOrDefault(s => s.Cat_Id.Equals(id));
            categorysum = categorysum - rslt_Pre_C_Amt.Catexplimit; // New Sum of Category

            var totalCategorysum = categorysum + catobj.Catexplimit;

            if (ModelState.IsValid) 
            {
                 if(totalCategorysum > total.Expense_Limit_Amt)
                {
                    TempData["ResultOk"] = "Category Limit Edit should not be greater than Total Exp Limit";
                    return View();
                }
                else
                {
                    var dbcategory = db.Categories.FirstOrDefault(s => s.Cat_Id.Equals(id));
                    dbcategory.Catname = catobj.Catname;
                    dbcategory.Catexplimit = catobj.Catexplimit;
                    db.SaveChanges();

                 
                    TempData["Alert updatecat"] = "Category Updated Successfully!";
                    return RedirectToAction("Dashboard", "Expense");
                }

            }
            
                return View();
            
        }
        public ActionResult Delete(int id)
        {
            Category category = db.Categories.FirstOrDefault(c => c.Cat_Id == id);
            if (category != null)
            {
                db.Remove(category);
                db.SaveChanges();
                TempData["Alert deletecat"] = " Category Deleted Successfully!";
                return RedirectToAction("Dashboard", "Expense");
            }
            return View();
        }

       
    }
}
