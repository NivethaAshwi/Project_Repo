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
        private readonly VisitorCategoryDBContext _dbforVLdetails;
        public VisitorLogService(VisitorCategoryDBContext dBContext)
        {
            _dbforVLdetails = dBContext;
        }
        public async Task CreateVisitorlog(VisitorLogsDetails visitors)
        {
           
            await _dbforVLdetails.LogDetails.AddAsync(visitors);
            
            await save();
        }

        public async Task Deletevisitorylog(VisitorLogsDetails visitordetails)
        {
            _dbforVLdetails.LogDetails.Remove(visitordetails);
            await save();
        }

        public async Task<List<VisitorLogsDetails>> GetAllVisitorLogs()
        {
            var visitorlogs =   _dbforVLdetails.LogDetails.ToList();
            return visitorlogs;

        }

        public async Task<VisitorLogsDetails> GetVisitorlogbyid(int id)
        {
            var logbyid = await _dbforVLdetails.LogDetails.FindAsync(id);
            return logbyid;
        }

        public async Task save()
        {
            await _dbforVLdetails.SaveChangesAsync();
            
        }

        public async Task Updatevisitorlog(VisitorLogsDetails visitordetails)
        {
             _dbforVLdetails.LogDetails.Update(visitordetails);
            await save();
        }
    }
}
