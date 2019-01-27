using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class UserProductModel
    {
        [Display(Name = "Product ID")]
        public int Id { get; set; }

        [Display(Name = "Product Name")]
        [Required(ErrorMessage = "Product Name field is required")]
        public string ProductName { get; set; }

        [Display(Name = "Category")]
        public string Category { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = "Price field is required")]
        public decimal Price { get; set; }

        [Display(Name = "Model")]
        public string Model { get; set; }
    }
}