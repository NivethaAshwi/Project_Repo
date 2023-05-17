using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VisitorManagement.DataAccess;
using VisitorManagement.Models;

namespace VisitorManagement.Service.IRepoInfo
{
    #region Visitor Category Details
    public interface ICategoryRepo
    {
        Task<List<VisitorCategoryDetails>> GetAllCategories();
        Task <List<VisitorCategoryDetails>> GetCategoryById(int id);
        Task CreateCategory(VisitorCategoryDetails category);
        Task UpdateCategory(VisitorCategoryDetails categoryDetails);
        Task DeleteCategory(VisitorCategoryDetails visitorCategory);
        Task <VisitorCategoryDetails> FindCategoriesById(int id);
        Task save();
        bool IsCategoryNameExists(string name);


    }
    #endregion
    #region Visistor details
    public interface IVisitorRepo
    {
        Task<List<Visitordetails>> GetAllVisitors();
        Task  <List<Visitordetails>> GetVisitorById(int id);
        Task CreateVisitor(Visitordetails visitors);
        Task UpdateVisitor(Visitordetails visitordetails);
        Task DeleteVisitor(Visitordetails visitordetails);
        Task<IEnumerable<Visitordetails>> SearchByVisitorDetails(string Mobileno, string vehicleno);
        Task<Visitordetails> FindVisitorById(int id);

        Task save();
    }
    #endregion
    #region Resident details
    public interface  IResidentRenpo
    {
        Task<IEnumerable<ResidentDetails>> SearchByDetails(string FlatID,string FN,string LN);
        Task<List<ResidentDetails>> GetAllResident();
        Task<List<ResidentDetails>>GetResidentById(int id);
        Task CreateResident(ResidentDetails visitors);
        Task UpdateResident(ResidentDetails visitordetails);
        Task DeleteResident(ResidentDetails visitordetails);
        Task<ResidentDetails> FindResidentsInfo(int id);
        
        Task save();
    }
    #endregion
    #region Visitor Log
    public interface IVisitorLogRepo
    {
        Task<List<VisitorLogsDetails>> GetAllVisitorLogs();
        Task<VisitorLogsDetails> GetVisitorLogById(int id);
        Task CreateVisitorLog(VisitorLogsDetails visitors);
        Task UpdateVisitorLog(VisitorLogsDetails visitordetails);
        Task DeleteVisitoryLog(VisitorLogsDetails visitordetails);

        Task save();
    }
    #endregion

}
