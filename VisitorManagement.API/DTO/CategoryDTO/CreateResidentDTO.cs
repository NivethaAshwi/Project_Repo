using System.ComponentModel.DataAnnotations;
using VisitorManagement.Models;

namespace VisitorManagement.API.DTO.CategoryDTO
{
    public class CreateResidentDTO
    {
        
        public string Flatnumber { get; set; }
        
        public string FirstName { get; set; }
       
        public string LastName { get; set; }
    }
    public class GetResidentDTO
    {
        public int ResidentId { get; set; }
        
        public string Flatnumber { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
       
        public ICollection<GetLogsDTO> LogDetails { get; } = new List<GetLogsDTO>(); // Collection navigation containing dependents


    }
    public class UpdateResidentDTO
    {
        public int ResidentId { get; set; }
      
        public string Flatnumber { get; set; }
       
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
    }
}
