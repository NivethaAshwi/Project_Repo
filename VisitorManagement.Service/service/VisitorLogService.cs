using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorManagement.DataAccess;
using VisitorManagement.Models;
using VisitorManagement.Service.IRepoInfo;

namespace VisitorManagement.Service.service
{
    public class VisitorLogService : IVisitorLogRepo
     
    {
        private readonly VisitorCategoryDBContext _dbforVLdetails; //VL - Visitor Logs
        public VisitorLogService(VisitorCategoryDBContext dBContext)
        {
            _dbforVLdetails = dBContext;
        }
        public async Task CreateVisitorLog(VisitorLogsDetails visitors)
        {
           
            await _dbforVLdetails.LogDetails.AddAsync(visitors);
            
            await save();
        }

        public async Task DeleteVisitoryLog(VisitorLogsDetails visitordetails)
        {
            _dbforVLdetails.LogDetails.Remove(visitordetails);
            await save();
        }

        public async Task<List<VisitorLogsDetails>> GetAllVisitorLogs()
        {
            var visitorLogs =   _dbforVLdetails.LogDetails.ToList();
            return visitorLogs;

        }

        public async Task<VisitorLogsDetails> GetVisitorLogById(int id)
        {
            var visitorLogs = await _dbforVLdetails.LogDetails.FindAsync(id);
            return visitorLogs;
        }

        public async Task save()
        {
            await _dbforVLdetails.SaveChangesAsync();
            
        }

        public async Task UpdateVisitorLog(VisitorLogsDetails visitordetails)
        {
             _dbforVLdetails.LogDetails.Update(visitordetails);
            await save();
        }
    }
}
