using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Exptracker2.Models
{
    public class Expense
    {
        [Key]
        public int Exp_Id { get; set; }

     
        [Required(ErrorMessage = "Title is Required")]
        [StringLength(maximumLength: 100, MinimumLength = 2)]
        public String Title { get; set; }

       

        [Required(ErrorMessage = "Description is Required")]
        [StringLength(maximumLength: 100, MinimumLength = 2)]
        public String Descr { get; set; }

        public DateTime? Expense_date { get; set; } = DateTime.Now;


        [Required(ErrorMessage = "Enter Amount!")]
        public int Amt { get; set; }



        //Navigation Properties
        [Display(Name = "Category")]
        public int Cat_Id { get; set; }
        [ForeignKey("Cat_Id")]
        public virtual Category? Category { get; set; }
    }
}
