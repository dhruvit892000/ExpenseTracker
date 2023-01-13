using System.ComponentModel.DataAnnotations;

namespace Exptracker2.Models
{
    public class TotalExplim
    {
        [Key]
        public int ExpLimit_Id { get; set; }
        [Required]
        public int Expense_Limit_Amt { get; set; }
    }
}
