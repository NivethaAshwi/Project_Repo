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
    public class VisitorDetailService : IVisitorRepository
    {
        private readonly VisitorCategoryDBContext _visitorsDbContext;
        public VisitorDetailService(VisitorCategoryDBContext dbforVDContext)
        {
            _visitorsDbContext = dbforVDContext;
        }
       public async Task CreateVisitor(VisitorDetails visitors)
        {

            await _visitorsDbContext.Visitordetails.AddAsync(visitors);
            await Save();

        }
        public async Task DeleteVisitor(VisitorDetails visitordetails)
        {
            _visitorsDbContext.Visitordetails.Remove(visitordetails);
            await Save();
        }

        public async Task<VisitorDetails> FindVisitorById(int id)
        {
            var visitorDetails = await _visitorsDbContext.Visitordetails.FindAsync(id);
            return visitorDetails;
        }

        public async Task<List<VisitorDetails>> GetAllVisitors()
        {
          //  var visitors = await _visitorsDbContext.Visitordetails.Include(a => a.VisitorLog).ToListAsync();
            var visitors = await _visitorsDbContext.Visitordetails.ToListAsync();
            return visitors;
        }
        public async Task<List<VisitorDetails>> GetVisitorById(int id)
        {
            var visitorById = await _visitorsDbContext.Visitordetails.Where(a => a.VisitorId == id).Include(_ => _.VisitorLog).ToListAsync();
             return visitorById;
        }

        public async Task Save()
        {
            await _visitorsDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<VisitorDetails>> SearchByVisitorDetails(string Mobileno, string vehicleno)
        {
            IQueryable<VisitorDetails> query = _visitorsDbContext.Visitordetails;

            if (!string.IsNullOrEmpty(Mobileno))
            {
                query = query.Where(e => e.MobileNo.Contains(Mobileno));

            }
            if (!string.IsNullOrEmpty(vehicleno))
            {
                query = query.Where(e => e.VehicleNo.Contains(vehicleno));
            }

            return await query.ToListAsync();
        }
        public async Task UpdateVisitor(VisitorDetails visitordetails)
        {
            _visitorsDbContext.Visitordetails.Update(visitordetails);
            await Save();
        }

    }
}
