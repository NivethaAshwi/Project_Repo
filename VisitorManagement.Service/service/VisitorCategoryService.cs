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
    public class VisitorCategoryService : ICategoryRepository
    {
        private readonly VisitorCategoryDBContext _categoryDbContext;
        public VisitorCategoryService(VisitorCategoryDBContext dbcontext)
        {
            _categoryDbContext = dbcontext;
        }
        public async Task CreateCategory(VisitorCategoryDetails category)
        {
            
            await _categoryDbContext.VisitorCategoryDetail.AddAsync(category);
            await Save();

        }
        public async Task DeleteCategory(VisitorCategoryDetails visitorCategory)
        {
            _categoryDbContext.VisitorCategoryDetail.Remove(visitorCategory);
            await Save();
        }
        public async Task<VisitorCategoryDetails> FindCategoriesById(int id)
        {
            var categoryDetails = await _categoryDbContext.VisitorCategoryDetail.FindAsync(id);
            return categoryDetails;
        }
        public async Task<List<VisitorCategoryDetails>> GetAllCategories()
        {
            var categories = await _categoryDbContext.VisitorCategoryDetail.ToListAsync();
            return categories;
            //var categories = await _DBforCDcontext.VisitorCategoryDetail.Include(visitor => visitor.Visitordetails).ToListAsync(); 
            //return categories;
        }

        public async Task<List<VisitorCategoryDetails>> GetCategoryById(int id)
        {
            var category = await _categoryDbContext.VisitorCategoryDetail.Where(a => a.VisitorCategoryId == id).Include(visitor => visitor.Visitordetails).ToListAsync();
            return category;
        }

        public bool IsCategoryNameExists(string name)
        {
            var result = _categoryDbContext.VisitorCategoryDetail.Where(x => x.CategoryName.ToLower().Trim() == name.ToLower().Trim()).Any();
            return result;
        }

        public async Task Save()            

        {
            await _categoryDbContext.SaveChangesAsync();
        }

        public async Task UpdateCategory(VisitorCategoryDetails categoryDetails)
        {
            _categoryDbContext.VisitorCategoryDetail.Update(categoryDetails);
            await Save(); 
        }
    }
}
