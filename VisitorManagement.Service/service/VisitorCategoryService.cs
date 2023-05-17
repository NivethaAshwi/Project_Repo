using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorManagement.DataAccess;
using VisitorManagement.Models;
using VisitorManagement.Service.IRepoInfo;

namespace VisitorManagement.Service.service
{
    public class VisitorCategoryService : ICategoryRepo
    {
        private readonly VisitorCategoryDBContext _DBforCDcontext; //CD - Categorydetails
        public VisitorCategoryService(VisitorCategoryDBContext dbcontext)
        {
            _DBforCDcontext = dbcontext;
        }
        public async Task CreateCategory(VisitorCategoryDetails category)
        {
            
            await _DBforCDcontext.VisitorCategoryDetail.AddAsync(category);
            await save();

        }
        public async Task DeleteCategory(VisitorCategoryDetails visitorCategory)
        {
            _DBforCDcontext.VisitorCategoryDetail.Remove(visitorCategory);
            await save();
        }
        public async Task<VisitorCategoryDetails> FindCategoriesById(int id)
        {
            var categoryDetails = await _DBforCDcontext.VisitorCategoryDetail.FindAsync(id);
            return categoryDetails;
        }
        public async Task<List<VisitorCategoryDetails>> GetAllCategories()
        {
            var categories = await _DBforCDcontext.VisitorCategoryDetail.Include(visitor => visitor.Visitordetails).ToListAsync(); 
            return categories;
        }

        public async Task<List<VisitorCategoryDetails>> GetCategoryById(int id)
        {
            var category = await _DBforCDcontext.VisitorCategoryDetail.Where(a => a.VisitorCategoryId == id).Include(visitor => visitor.Visitordetails).ToListAsync();
            return category;
        }

        public bool IsCategoryNameExists(string name)
        {
            var result = _DBforCDcontext.VisitorCategoryDetail.Where(x => x.CategoryName.ToLower().Trim() == name.ToLower().Trim()).Any();
            return result;
        }

        public async Task save()            

        {
            await _DBforCDcontext.SaveChangesAsync();
        }

        public async Task UpdateCategory(VisitorCategoryDetails categoryDetails)
        {
            _DBforCDcontext.VisitorCategoryDetail.Update(categoryDetails);
            await save(); 
        }
    }
}
