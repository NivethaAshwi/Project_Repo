using System.ComponentModel.DataAnnotations;
using VisitorManagement.API.DTO.CategoryDTO;
using VisitorManagement.Models;

namespace VisitorManagement.API.DTO
{
    public class CreateCategoryDTO  // dto for hiding the unnecessory implement to the user ex:PK no need toshow to the user
    {
        // public int VisitorCategoryId { get; set; }
        [Required(ErrorMessage = "CategoryName is required")]
        [MaxLength(30)]
        public string CategoryName { get; set; }

        public string categoryDescription { get; set; } 

    }
    public class UpdateCategorDTO
    {

        public int VisitorCategoryId { get; set; }

        public string CategoryName { get; set; }

        public string categoryDescription { get; set; }
        // public ICollection<Visitordetails> Visitordetails { get; } = new List<Visitordetails>(); // Collection navigation containing dependents

    }
    public class VisitorCategoryDTO
    {

        public int VisitorCategoryId { get; set; }

        public string CategoryName { get; set; }
        

        public string categoryDescription { get; set; }
        public ICollection<GetcategoryVisitorDTO> Visitordetails { get; } = new List<GetcategoryVisitorDTO>();
    }
}
