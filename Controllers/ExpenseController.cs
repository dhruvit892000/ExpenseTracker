using Exptracker2.Data;
using Exptracker2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Exptracker2.Controllers
{
    public class ExpenseController : Controller
    {
        AppDbContext db;
        
        public ExpenseController(AppDbContext _db)
        {
            this.db = _db;
        }

            
        public ActionResult Dashboard(int? id)
        {

            List<Category> catlist = db.Categories.ToList();
            ViewBag.CatTbls = new SelectList(catlist, "Cat_Id", "Catname");
           
                Mergemod md = new Mergemod();
                md.cat = db.Categories.ToList();
                md.texplim = db.TotalExplims.ToList();
               
                
                
                if(db.TotalExplims.IsNullOrEmpty())
                {
                    TempData["texpl"] = 0;
                }
                else
                {
                    var totalexplim = db.TotalExplims.Take(1).FirstOrDefault();
                    var demo = totalexplim.Expense_Limit_Amt;
                    TempData["texpl"] = demo;
                }
               
                if(id == null)
                {
                    md.exp = db.Expenses.ToList();
                }
                else
                {
                    md.exp = db.Expenses.Where(z=> z.Cat_Id== id).ToList();
                }
              
                    return View(md);

          
        }

      
        public ActionResult Expform()
        {
            var categoryList = db.Categories.Select(x => new Category()
            {

                Cat_Id = x.Cat_Id,
                Catname = x.Catname
            }).ToList(); 

            ViewBag.CategoryList = new SelectList(categoryList, "Cat_Id", "Catname");

             return View();
         }

         [HttpPost]
         public ActionResult Expform(Expense exps)
         {
            var expenID = exps.Cat_Id;
            var filteredResult = (from s in db.Categories
                                  where s.Cat_Id == expenID
                                  select new { s.Catexplimit }).Single();
            var cateExpense = filteredResult.Catexplimit; 


            var total = db.Expenses.Where(c => c.Cat_Id == expenID).Select(x => x.Amt).Sum();

            var TotalExpenseCategory = total + exps.Amt;
            
            if (ModelState.IsValid)
             {
                

                if (TotalExpenseCategory > cateExpense)
                {
                   /* categoryList = db.Categories.Select(x => new Category()
                    {

                        Cat_Id = x.Cat_Id,
                        Catname = x.Catname
                    }).ToList();

                    ViewBag.CategoryList = new SelectList(categoryList, "Cat_Id", "Catname");*/

                    TempData["AlertMsg"] = $"Your Expense Amount should be less then  {cateExpense} (Category Limit) !";
                    return View(exps);
                }

                else
                {
                    db.Expenses.Add(exps);
                    db.SaveChanges();
                    TempData["Alert message"] = "New Expense Added Successfully!";
                    return RedirectToAction("Dashboard");
                }
             }
            var categoryList = db.Categories.Select(x => new Category()
            {

                Cat_Id = x.Cat_Id,
                Catname = x.Catname
            }).ToList(); 

            ViewBag.CategoryList = new SelectList(categoryList, "Cat_Id", "Catname");

            return View(exps);

         }
         public ActionResult Editexp(int id)
         {
             List<Category> catlist = db.Categories.ToList();
             ViewBag.CatTbl = new SelectList(catlist, "Cat_Id", "Catname");


             return View(db.Expenses.Where(c => c.Exp_Id == id).FirstOrDefault());
         }
         [HttpPost]
         public ActionResult Editexp(int id, Expense exp)
         {
             if (ModelState.IsValid)
             {
                var categoryList = db.Categories.Select(x => new Category()
                {

                    Cat_Id = x.Cat_Id,
                    Catname = x.Catname
                }).ToList(); 

                ViewBag.CategoryList = new SelectList(categoryList, "Cat_Id", "Catname");
                db.Entry(exp).State = EntityState.Modified;
                 db.SaveChanges();
                 TempData["Alert update"] = " Expense Data Updated Successfully!";
                 return RedirectToAction("Dashboard");
             }

             return View();

         }
         public ActionResult Deleteexp(int id)
         {
             Expense exps = db.Expenses.FirstOrDefault(c => c.Exp_Id == id);
             if (exps != null)
             {
                 db.Remove(exps);
                 db.SaveChanges();
                 TempData["Alert delete"] = " Expense Data Deleted Successfully!";
                 return RedirectToAction("Dashboard");
             }
             return View();
         }
        

     }
}
