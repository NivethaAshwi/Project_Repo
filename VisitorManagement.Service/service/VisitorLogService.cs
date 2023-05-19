using Microsoft.EntityFrameworkCore;
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
    public class VisitorLogService : IVisitorLogRepository

    {
        private readonly VisitorCategoryDBContext _visitorLogDbContext;
        public VisitorLogService(VisitorCategoryDBContext dBContext)
        {
            _visitorLogDbContext = dBContext;
        }
        public async Task CreateVisitorLog(VisitorLogsDetails visitors)
        {
           
            await _visitorLogDbContext.LogDetails.AddAsync(visitors);
            
            await Save();
        }

        public async Task DeleteVisitoryLog(VisitorLogsDetails visitordetails)
        {
            _visitorLogDbContext.LogDetails.Remove(visitordetails);
            await Save();
        }

        public async Task<List<VisitorLogsDetails>> GetAllVisitorLogs()
        {
            var visitorLogs = await _visitorLogDbContext.LogDetails.ToListAsync();
            return visitorLogs;

        }

        public async Task<VisitorLogsDetails> GetVisitorLogById(int id)
        {
            var visitorLogs = await _visitorLogDbContext.LogDetails.FindAsync(id);
            return visitorLogs;
        }

        public async Task Save()
        {
            await _visitorLogDbContext.SaveChangesAsync();
            
        }

        public async Task UpdateVisitorLog(VisitorLogsDetails visitordetails)
        {
            _visitorLogDbContext.LogDetails.Update(visitordetails);
            await Save();
        }
    }
}
