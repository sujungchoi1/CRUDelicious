using System;
using System.ComponentModel.DataAnnotations;
namespace CRUDelicious.Models
{
    public class Dish
    {
        [Key]
        public int DishId { get;set; }

        [Required]
        [Display(Name = "Name of Dish")]
        public string Name { get; set; }
        
        [Required]
        [Display(Name = "Chef's Name")]
        public string Chef { get; set; }
        
        [Required(ErrorMessage="Tastiness required")]
        [Range(1, 5, ErrorMessage="Tastiness must be between 1-5")]
        public int Tastiness { get; set; }

        [Required(ErrorMessage="Calories required")]
        [Range(0, 5000, ErrorMessage="Calories must be > 0")]
        [Display(Name ="# of Calories")]
        public int Calories { get; set; }
        // [Required(ErrorMessage="Description required")]
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}