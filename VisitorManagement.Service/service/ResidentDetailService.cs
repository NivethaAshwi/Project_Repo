using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VisitorManagement.DataAccess;
using VisitorManagement.Models;
using VisitorManagement.Service.IRepoInfo;

namespace VisitorManagement.Service.service
{
    public class ResidentDetailService : IResidentRepository
    {
        private readonly VisitorCategoryDBContext _residentDbContext;  
        public ResidentDetailService(VisitorCategoryDBContext dBContext)
        {
            _residentDbContext = dBContext;
        }
        public async Task CreateResident(ResidentDetails visitors)
        {
            await _residentDbContext.ResidentDetails.AddAsync(visitors);
            await Save();
        }
        public async Task DeleteResident(ResidentDetails visitordetails)
        {
            _residentDbContext.ResidentDetails.Remove(visitordetails);
            await Save();
        }
        public async Task<List<ResidentDetails>> GetAllResident()
        {
            var residents = await _residentDbContext.ResidentDetails.ToListAsync();
            // var residents = await _dbforRDdetails.ResidentDetails.Include(a => a.LogDetails).ToListAsync()
            return residents;
        }
        public async Task<List<ResidentDetails>> GetResidentById(int id)
        {
            var residentbyId = await _residentDbContext.ResidentDetails.Where(a => a.ResidentId == id).Include(_ => _.LogDetails).ToListAsync();
            return residentbyId;
        }
        public async Task Save()
        {
            await _residentDbContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<ResidentDetails>> SearchByDetails(string FlatID, string FN, string LN)
        {
            IQueryable<ResidentDetails> query = _residentDbContext.ResidentDetails;
            if(!string.IsNullOrEmpty(FlatID))
            {
                query = query.Where(d => d.Flatnumber.Contains(FlatID));
            }
           if (!string.IsNullOrEmpty(FN))
            {
                query = query.Where(e => e.FirstName.Contains(FN));
                            
            }
            if (!string.IsNullOrEmpty(LN))
            {
                query = query.Where(e => e.LastName.Contains(LN));
            }

            return await query.ToListAsync();
        }
        public async Task<ResidentDetails> FindResidentsInfo(int id)
        {

            var findRD = await _residentDbContext.ResidentDetails.FindAsync(id);
            return findRD;

        }
        public async Task UpdateResident(ResidentDetails visitordetails)
        {
            _residentDbContext.ResidentDetails.Update(visitordetails);
            await Save();
        }
    }
}
