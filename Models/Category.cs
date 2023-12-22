using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVC_ecom.Model
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]

        [DisplayName("Category Name")]
        public string Name { get; set; } 

        [DisplayName("Display order")]
        public int DisplayOrderNumber { get; set; }
    }
}
