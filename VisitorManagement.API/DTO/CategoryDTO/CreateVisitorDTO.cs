using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using VisitorManagement.Models;

namespace VisitorManagement.API.DTO.CategoryDTO
{
    public class CreateVisitorDTO
    {
        public string VisitorFirstName { get; set; }
        public string VisitorLastName { get; set; }
        public string MobileNo { get; set; }
        public string VehicleNo { get; set; }
        public string Reason { get; set; }

        public int VisitorCategoryId { get; set; }
    }
    public class GetVisitorDTO
    {

        public int VisitorId { get; set; }

        public string VisitorFirstName { get; set; }

        public string VisitorLastName { get; set; }


        //public VisitorCategoryDetails VisitorCategoryDetails { get; set; }// Required foreign key property

        public string MobileNo { get; set; }

        public string VehicleNo { get; set; }

        public string Reason { get; set; }
        public int VisitorCategoryId { get; set; }
        public ICollection<GetLogsDTO> VisitorLog { get; } = new List<GetLogsDTO>();
    }
    public class GetcategoryVisitorDTO
    {

        public int VisitorId { get; set; }

        public string VisitorFirstName { get; set; }

        public string VisitorLastName { get; set; }


        //public VisitorCategoryDetails VisitorCategoryDetails { get; set; }// Required foreign key property

        public string MobileNo { get; set; }

        public string VehicleNo { get; set; }

        public string Reason { get; set; }
        public int VisitorCategoryId { get; set; }
    }
        public class UpdateVisitorDTO
    {
        public int VisitorId { get; set; }

        public string VisitorFirstName { get; set; }

        public string VisitorLastName { get; set; }


        // public VisitorCategoryDetails VisitorCategoryDetails { get; set; }
        public int VisitorCategoryId { get; set; }

        public string MobileNo { get; set; }

        public string VehicleNo { get; set; }

        public string Reason { get; set; }
    }
}
