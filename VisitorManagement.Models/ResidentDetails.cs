using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorManagement.Models
{

    public class ResidentDetails
    {
        [Key]
        public int ResidentId { get; set; }
        [Required]
        public string Flatnumber { get; set; } = string.Empty;
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        public ICollection<VisitorLogsDetails> LogDetails { get; } = new List<VisitorLogsDetails>(); // Collection navigation containing dependents


    }
    public class VisitorLogsDetails
    {
        [Key]
        public int VisitorLogId { get; set; }
        [Required]
        public DateTime EntryTime { get; set; }
        [Required]
        public DateTime ExitTime { get; set; }
        public DateTime SpentTime { get; set; }
        [Required]
        [ForeignKey("ResidentId")]
        public int ResidentId { get; set; }
        [Required]
        [ForeignKey("VisitorId")]
        public int VisitorId { get; set; }
        public ResidentDetails ResidentDetails { get; set; }
        public VisitorDetails visitordetails { get; set; }
    }
}
