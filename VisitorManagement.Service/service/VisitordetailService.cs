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
        private readonly VisitorCategoryDBContext _DBforVDcontext;
        public VisitordetailService(VisitorCategoryDBContext dbforVDContext)
        {
            _DBforVDcontext = dbforVDContext;
        }
        //public async Task CreateVisitor(Visitordetails visitors)
        //{
        //    await _DBforVDcontext.Visitordetails.AddAsync(visitors);
        //    await save();

        //}
        public async Task CreateVisitor(Visitordetails visitors)
        {

            await _DBforVDcontext.Visitordetails.AddAsync(visitors);
            await save();

        }

        public async Task Deletevisitory(Visitordetails visitordetails)
        {
          _DBforVDcontext.Visitordetails.Remove(visitordetails);
            await save();
            
        }

        public async Task<Visitordetails> Fetchdata(int id)
        {
            var orders = await _DBforVDcontext.Visitordetails.FindAsync(id);
            return orders;
        }

        public async Task<List<Visitordetails>> GetAllVisitors()
        {
            var visitors = await _DBforVDcontext.Visitordetails.Include(a => a.VisitorLog).ToListAsync();
            return visitors;
        }
        public async Task<List<Visitordetails>> GetVisitorbyid(int id)
        {
            var visitorbyid = await _DBforVDcontext.Visitordetails.Where(a => a.VisitorId == id).Include(_ => _.VisitorLog).ToListAsync();
             return visitorbyid;
        }

        public async Task save()
        {
            await _DBforVDcontext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Visitordetails>> Searchbyvisitor(string Mobileno, string vehicleno)
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
        public async Task Updatevisitor(Visitordetails visitordetails)
        {
            _DBforVDcontext.Visitordetails.Update(visitordetails);
            await save();
        }

    }
}
