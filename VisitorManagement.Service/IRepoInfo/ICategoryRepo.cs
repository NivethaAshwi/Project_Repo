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
        Task <List<VisitorCategoryDetails>> GetCategorybyId(int id);
        Task CreateCategory(VisitorCategoryDetails category);
        Task UpdateCategory(VisitorCategoryDetails categoryDetails);
        Task DeleteCategory(VisitorCategoryDetails visitorCategory);
        Task <VisitorCategoryDetails> finddata (int id);
        Task save();
        bool IsCategoryNameExists(string name);


    }
    #endregion
    #region Visistor details
    public interface IVisitorRepo
    {
        Task<List<Visitordetails>> GetAllVisitors();
        Task  <List<Visitordetails>> GetVisitorbyid(int id);
        Task CreateVisitor(Visitordetails visitors);
        Task Updatevisitor(Visitordetails visitordetails);
        Task Deletevisitory(Visitordetails visitordetails);
        Task<IEnumerable<Visitordetails>> Searchbyvisitor(string Mobileno, string vehicleno);
        Task<Visitordetails> Fetchdata(int id);

        Task save();
    }
    #endregion
    #region Resident details
    public interface  IResidentRenpo
    {
        Task<IEnumerable<ResidentDetails>> Searchbyresident(string FlatID,string FN,string LN);
        Task<List<ResidentDetails>> GetAllResident();
        Task<List<ResidentDetails>>GetResidentbyid(int id);
        Task CreateResident(ResidentDetails visitors);
        Task UpdateResident(ResidentDetails visitordetails);
        Task Deleteresident(ResidentDetails visitordetails);
        Task<ResidentDetails> searchresident(int id);
        
        Task save();
    }
    #endregion
    #region Visitor Log
    public interface IVisitorLogRepo
    {
        Task<List<VisitorLogsDetails>> GetAllVisitorLogs();
        Task<VisitorLogsDetails> GetVisitorlogbyid(int id);
        Task CreateVisitorlog(VisitorLogsDetails visitors);
        Task Updatevisitorlog(VisitorLogsDetails visitordetails);
        Task Deletevisitorylog(VisitorLogsDetails visitordetails);

        Task save();
    }
    #endregion

}
