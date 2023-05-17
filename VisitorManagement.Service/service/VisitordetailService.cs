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
    public class VisitordetailService : IVisitorRepo
    {
        private readonly VisitorCategoryDBContext _DBforVDcontext; // VD - Visitordetails
        public VisitordetailService(VisitorCategoryDBContext dbforVDContext)
        {
            _DBforVDcontext = dbforVDContext;
        }
       public async Task CreateVisitor(Visitordetails visitors)
        {

            await _DBforVDcontext.Visitordetails.AddAsync(visitors);
            await save();

        }
        public async Task DeleteVisitor(Visitordetails visitordetails)
        {
          _DBforVDcontext.Visitordetails.Remove(visitordetails);
            await save();
        }

        public async Task<Visitordetails> FindVisitorById(int id)
        {
            var visitorDetails = await _DBforVDcontext.Visitordetails.FindAsync(id);
            return visitorDetails;
        }

        public async Task<List<Visitordetails>> GetAllVisitors()
        {
            var visitors = await _DBforVDcontext.Visitordetails.Include(a => a.VisitorLog).ToListAsync();
            return visitors;
        }
        public async Task<List<Visitordetails>> GetVisitorById(int id)
        {
            var visitorById = await _DBforVDcontext.Visitordetails.Where(a => a.VisitorId == id).Include(_ => _.VisitorLog).ToListAsync();
             return visitorById;
        }

        public async Task save()
        {
            await _DBforVDcontext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Visitordetails>> SearchByVisitorDetails(string Mobileno, string vehicleno)
        {
            IQueryable<Visitordetails> query = _DBforVDcontext.Visitordetails;

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
        public async Task UpdateVisitor(Visitordetails visitordetails)
        {
            _DBforVDcontext.Visitordetails.Update(visitordetails);
            await save();
        }

    }
}
