using Microsoft.AspNetCore.Mvc;
using MovieWeb.Data;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MovieWeb.Areas.Admin.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0,10)]
        [DisplayName("IMDb")]
        public double Rating { get; set; }

        [Required]
        public string Director { get; set; }

        [Required]
        [DisplayName("List Price")]
        [Range(1, 1000)]
        public double ListPrice { get; set; }
    }
}
