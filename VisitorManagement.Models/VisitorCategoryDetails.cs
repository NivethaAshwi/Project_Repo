using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorManagement.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace VisitorManagement.Models
{
    public class VisitorCategoryDetails  //one to many relationship master table
    {
        [Key]
        public int VisitorCategoryId { get; set; }
        [Required(ErrorMessage = "CategoryName is required")]
        [MaxLength(30)]
        public string CategoryName { get; set; } = string.Empty;
        [MaxLength(40)]
        public string categoryDescription { get; set; } = string.Empty;
        public ICollection<VisitorDetails> Visitordetails { get; } = new List<VisitorDetails>(); // Collection navigation containing dependents
    }
    public class VisitorDetails  
    {
        [Key]
        public int VisitorId { get; set; }
        [Required(ErrorMessage = "VisitorFirstName is required")]
        [MaxLength(30)]
        public string VisitorFirstName { get; set; } = string.Empty;
        [Required(ErrorMessage = "VisitorLastName is required")]
        [MaxLength(40)]
        public string VisitorLastName { get; set; } = string.Empty;

        [ForeignKey("VisitorCategoryId")]
        public int VisitorCategoryId { get; set; }// Required foreign key property
        [Phone]
        [Required(ErrorMessage = "Mobile No is required")]
        public string MobileNo { get; set; } 
        [Required(ErrorMessage = "VehicleNo is required")]
        public string VehicleNo { get; set; }
        [MaxLength(30)]
        public string Reason { get; set; }
        public VisitorCategoryDetails VisitorCategoryDetails { get; set; } = null!; // Required reference navigation to principal
        public ICollection<VisitorLogsDetails> VisitorLog { get; } = new List<VisitorLogsDetails>(); // Collection navigation containing dependents


    }
}
