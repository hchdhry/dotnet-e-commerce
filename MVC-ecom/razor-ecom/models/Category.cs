using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace razor_ecom.models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]

        [DisplayName("Category Name")]
        [MaxLength(30, ErrorMessage = "name too long cannot exceed 30 character")]
        [MinLength(2, ErrorMessage = "name too short")]

        public string? Name { get; set; }


        [DisplayName("Display order")]
        [Range(1, 100)]
        public int DisplayOrderNumber { get; set; }
    }
}
