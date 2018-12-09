using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class UserProductModel
    {
        [Display(Name = "ID Produit")]
        [Required(ErrorMessage = "* This field is required")]
        public int Id { get; set; }

        [Display(Name = "Nom Produit")]
        [Required(ErrorMessage = "* This field is required")]
        public string ProductName { get; set; }

        [Display(Name = "Catégorie")]
        [Required(ErrorMessage = "* This field is required")]
        public string Category { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = "* This field is required")]
        public int Price { get; set; }

        [Display(Name = "Model")]
        [Required(ErrorMessage = "* This field is required")]
        public string Model { get; set; }
    }
}