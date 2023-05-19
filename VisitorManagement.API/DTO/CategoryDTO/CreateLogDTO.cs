using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace VisitorManagement.API.DTO.CategoryDTO
{
    public class CreateLogDTO
    {
        
        public DateTime EntryTime { get; set; }
        [Required]
        public DateTime ExitTime { get; set; } 

        // public DateTime SpentTime { get; set; }
        //var datetime = visitors.EntryTime.ToString("MM/dd/yyyy hh:mm tt");
        //    var datetime1 = visitors.ExitTime.ToString("MM/dd/yyyy hh:mm tt");// 08/04/2021 11:58 PM

        public int ResidentId { get; set; }

        public int VisitorId { get; set; }
    }
    public class GetLogsDTO
    {

        public int VisitorLogId { get; set; }

        public DateTime EntryTime { get; set; }

        public DateTime ExitTime { get; set; }

       // public DateTime SpentTime { get; set; }


        public int ResidentId { get; set; }

        public int VisitorId { get; set; }

    }
    public class UpdateDTO
    {

        public int VisitorLogId { get; set; }

        public DateTime EntryTime { get; set; }

        public DateTime ExitTime { get; set; }

       // public DateTime SpentTime { get; set; }


        public int ResidentId { get; set; }

        public int VisitorId { get; set; }
    }
}
