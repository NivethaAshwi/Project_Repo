using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.Metrics;
using VisitorManagement.DataAccess;
using VisitorManagement.Service;
using VisitorManagement.Models;
using AutoMapper;

using VisitorManagement.API.DTO;
using VisitorManagement.API.DTO.CategoryDTO;
using VisitorManagement.Service.service;
using VisitorManagement.Service.IRepoInfo;
using System.Text.Json;
using VisitorManagement.API.InputModels;

namespace VisitorManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitorCategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryService;
        private readonly IMapper _mapper;
        private readonly ILogger<VisitorCategoryController> _logger;
        private readonly IPaginationServices<CreateCategoryDTO, VisitorCategoryDetails> _paginationService;
        public VisitorCategoryController(ICategoryRepository categoryRepo, IMapper mapper,ILogger<VisitorCategoryController> logger,IPaginationServices<CreateCategoryDTO,VisitorCategoryDetails> paginationServices)
        {
            _categoryService = categoryRepo;
            _mapper = mapper;
            _logger = logger;
            _paginationService = paginationServices;
            
        }
        #region GetAll Visitor Category Details
        [HttpGet]
        public async Task<IActionResult> GetVisitorCategory()
        {
            try
            {
                _logger.LogInformation("Executing {Action}", nameof(GetVisitorCategory));
                var categories = await _categoryService.GetAllCategories();
                var categoryDto = _mapper.Map<List<VisitorCategoryDTO>>(categories);
                if (categories == null)
                {
                    _logger.LogError($"Error while try to get category record");
                    return NoContent();

                }
                return Ok(categoryDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                    "Error retrieving categorydetails data from the database");
            }
        }
        #endregion

        #region Get Visitor Category details by Id
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetVisitorCategoryById(int id)
        {
            try
            {
                _logger.LogInformation("Executing {Action} with id: {id}", nameof(GetVisitorCategoryById), id);
                var categories = await _categoryService.GetCategoryById(id);
                if (categories == null)
                {
                    _logger.LogError($"Error while try to get category record id:{id}");
                    return NoContent();

                }
                var categoryDto = _mapper.Map<List<VisitorCategoryDTO>>(categories);
                return Ok(categoryDto);
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                  "Error retrieving data from VisitorCategory based on Id");
            }
        }
        #endregion

        #region Create new Visitor Category
        [HttpPost]
        public async Task<IActionResult> Addcategory([FromBody] CreateCategoryDTO addcategoryRequest)
        {
            try
            {
                _logger.LogInformation("Executing {Action} ", nameof(Addcategory));
                if (ModelState.IsValid)
                {
                    var result = _categoryService.IsCategoryNameExists(addcategoryRequest.CategoryName);
                    if (result)
                    {
                        return Conflict("Category Name already exists");
                    }
                    var category = _mapper.Map<VisitorCategoryDetails>(addcategoryRequest);
                    await _categoryService.CreateCategory(category);//instead of hardcoding  we can do automapper like this
                    return CreatedAtAction("Addcategory", new { id = category.VisitorCategoryId }, category);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch(Exception)
              {
                return StatusCode(StatusCodes.Status500InternalServerError,
           "Error creating new visitorcategory record");
              }

        }
        #endregion

        #region Update Existing Visitor Category
        [HttpPut]
        public async Task<IActionResult> UpdateCategory(UpdateCategorDTO categoryupdate)
        {
           
            try
            {
                _logger.LogInformation("Executing {Action}", nameof(UpdateCategory));
                if (categoryupdate == null)
                {
                    _logger.LogError($"Error while try to update category record");
                    return BadRequest();
                }
                var category = _mapper.Map<VisitorCategoryDetails>(categoryupdate);
                await _categoryService.UpdateCategory(category);
                return NoContent();
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error updating category details data");
            }
        }
        #endregion

        #region Delete Visitor Category By Id
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteCategoryById(int id)
        {
            try
            {
                _logger.LogInformation("Executing {Action} with id: {id}", nameof(DeleteCategoryById), id);
                if (id == 0)
                {
                    _logger.LogError($"Error while try to delete category record list");
                    return BadRequest();
                }
                var categoryDetails = await _categoryService.FindCategoriesById(id);

                await _categoryService.DeleteCategory(categoryDetails);
                return NoContent();
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error DeleteCategorydetails data");
            }
        }
        #endregion

        #region Pagination for Visitor Category
        [HttpPost]
        [Route("GetPagination")]
        public async Task<ActionResult> GetPaginationDetailsforCategory([FromBody]PaginationInput paginationInput)
        {
            var category = await _categoryService.GetAllCategories();
            var result = _paginationService.GetPagination(category,paginationInput);
            return Ok(result);
        }
        #endregion
    }
}

