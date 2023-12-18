using System.ComponentModel.DataAnnotations;

namespace MVC_ecom.Model
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } 
        public int DisplayOrderNumber { get; set; }
    }
}
