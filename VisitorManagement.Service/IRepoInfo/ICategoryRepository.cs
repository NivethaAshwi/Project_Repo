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
    public interface ICategoryRepository
    {
        Task<List<VisitorCategoryDetails>> GetAllCategories();
        Task <List<VisitorCategoryDetails>> GetCategoryById(int id);
        Task CreateCategory(VisitorCategoryDetails category);
        Task UpdateCategory(VisitorCategoryDetails categoryDetails);
        Task DeleteCategory(VisitorCategoryDetails visitorCategory);
        Task <VisitorCategoryDetails> FindCategoriesById(int id);
        Task Save();
        bool IsCategoryNameExists(string name);


    }
    #endregion
    #region Visistor details
    public interface IVisitorRepository
    {
        Task<List<VisitorDetails>> GetAllVisitors();
        Task  <List<VisitorDetails>> GetVisitorById(int id);
        Task CreateVisitor(VisitorDetails visitors);
        Task UpdateVisitor(VisitorDetails visitordetails);
        Task DeleteVisitor(VisitorDetails visitordetails);
        Task<IEnumerable<VisitorDetails>> SearchByVisitorDetails(string Mobileno, string vehicleno);
        Task<VisitorDetails> FindVisitorById(int id);

        Task Save();
    }
    #endregion
    #region Resident details
    public interface IResidentRepository
    {
        Task<IEnumerable<ResidentDetails>> SearchByDetails(string FlatID,string FN,string LN);
        Task<List<ResidentDetails>> GetAllResident();
        Task<List<ResidentDetails>>GetResidentById(int id);
        Task CreateResident(ResidentDetails visitors);
        Task UpdateResident(ResidentDetails visitordetails);
        Task DeleteResident(ResidentDetails visitordetails);
        Task<ResidentDetails> FindResidentsInfo(int id);
        
        Task Save();
    }
    #endregion
    #region Visitor Log
    public interface IVisitorLogRepository
    {
        Task<List<VisitorLogsDetails>> GetAllVisitorLogs();
        Task<VisitorLogsDetails> GetVisitorLogById(int id);
        Task CreateVisitorLog(VisitorLogsDetails visitors);
        Task UpdateVisitorLog(VisitorLogsDetails visitordetails);
        Task DeleteVisitoryLog(VisitorLogsDetails visitordetails);

        Task Save();
    }
    #endregion

}
