using AutoMapper;
using VisitorManagement.API.DTO;
using VisitorManagement.API.DTO.CategoryDTO;

using VisitorManagement.Models;


namespace VisitorManagement.API.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // source and destination to dest to source
            CreateMap<VisitorCategoryDetails, CreateCategoryDTO>().ReverseMap(); //Vice versa 
            CreateMap<VisitorCategoryDetails, VisitorCategoryDTO>().ReverseMap();
            CreateMap<VisitorCategoryDetails, UpdateCategorDTO>().ReverseMap();

            //visitor
            CreateMap<VisitorDetails, GetVisitorDTO>().ReverseMap(); //Vice versa 
            CreateMap<VisitorDetails, CreateVisitorDTO>().ReverseMap();
            CreateMap<VisitorDetails, UpdateVisitorDTO>().ReverseMap();
            CreateMap<VisitorDetails, GetcategoryVisitorDTO>().ReverseMap();
            //Resident
            CreateMap<ResidentDetails, CreateResidentDTO>().ReverseMap();
            CreateMap<ResidentDetails, GetResidentDTO>().ReverseMap();
            CreateMap<ResidentDetails, UpdateResidentDTO>().ReverseMap();
            //Logdetails
            CreateMap<VisitorLogsDetails, CreateLogDTO>().ReverseMap();
            CreateMap<VisitorLogsDetails,GetLogsDTO>().ReverseMap();
            CreateMap<VisitorLogsDetails, UpdateDTO>().ReverseMap();


        }
    }
}
