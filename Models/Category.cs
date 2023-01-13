using System.ComponentModel.DataAnnotations;

namespace Exptracker2.Models
{
    public class Category
    {
        [Key]
        public int Cat_Id { get; set; }
        [Required]
        public String Catname { get; set; }
        [Required]
        public int Catexplimit { get; set; }
        public List<Expense>? Expenses { get; set; }

    }
}
