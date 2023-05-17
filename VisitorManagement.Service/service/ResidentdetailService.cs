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
    public class ResidentdetailService : IResidentRenpo
    {
        private readonly VisitorCategoryDBContext _dbforRDdetails;  //RD - Resident details
        public ResidentdetailService(VisitorCategoryDBContext dBContext)
        {
            _dbforRDdetails = dBContext;
        }
        public async Task CreateResident(ResidentDetails visitors)
        {
            await _dbforRDdetails.ResidentDetails.AddAsync(visitors);
            await save();
        }
        public async Task DeleteResident(ResidentDetails visitordetails)
        {
            _dbforRDdetails.ResidentDetails.Remove(visitordetails);
            await save();
        }
        public async Task<List<ResidentDetails>> GetAllResident()
        {
            var residents = await _dbforRDdetails.ResidentDetails.Include(a => a.LogDetails).ToListAsync();
            return residents;
        }
        public async Task<List<ResidentDetails>> GetResidentById(int id)
        {
            var residentbyId = await _dbforRDdetails.ResidentDetails.Where(a => a.ResidentId == id).Include(_ => _.LogDetails).ToListAsync();
            return residentbyId;
        }
        public async Task save()
        {
            await _dbforRDdetails.SaveChangesAsync();
        }
        public async Task<IEnumerable<ResidentDetails>> SearchByDetails(string FlatID, string FN, string LN)
        {
            IQueryable<ResidentDetails> query = _dbforRDdetails.ResidentDetails;
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

            var findRD = await _dbforRDdetails.ResidentDetails.FindAsync(id);
            return findRD;

        }
        public async Task UpdateResident(ResidentDetails visitordetails)
        {
            _dbforRDdetails.ResidentDetails.Update(visitordetails);
            await save();
        }
    }
}
