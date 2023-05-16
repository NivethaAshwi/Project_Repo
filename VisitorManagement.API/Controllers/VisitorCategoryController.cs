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

namespace VisitorManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitorCategoryController : ControllerBase
    {
        private readonly ICategoryRepo _serviceRep;
        private readonly IMapper _mapper;
        private readonly ILogger<VisitorCategoryController> _logger;
        public VisitorCategoryController(ICategoryRepo categoryRepo, IMapper mapper,ILogger<VisitorCategoryController> logger)
        {
            _serviceRep = categoryRepo;
            _mapper = mapper;
            _logger = logger;
            
        }
        [HttpGet]
        public async Task<IActionResult> GetVisitorcategorydetails()
        {
            try
            {
                _logger.LogInformation("Executing {Action}", nameof(GetVisitorCategorybyid));
                var categories = await _serviceRep.GetAllCategories();
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
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetVisitorCategorybyid(int id)
        {
            try
            {
                _logger.LogInformation("Executing {Action} with id: {id}", nameof(GetVisitorCategorybyid), id);
                var categories = await _serviceRep.GetCategorybyId(id);
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
        [HttpPost]
        public async Task<IActionResult> AddVisitorcategory([FromBody] CreateCategoryDTO addcategoryRequest)
        {
            try
            {
                
               

                _logger.LogInformation("Executing {Action} ", nameof(AddVisitorcategory));
                if (ModelState.IsValid)
                {
                   
                    var result = _serviceRep.IsCategoryNameExists(addcategoryRequest.CategoryName);
                    if (result)
                    {
                        return Conflict("Category Name already exists");
                    }
                    var category = _mapper.Map<VisitorCategoryDetails>(addcategoryRequest);
                    await _serviceRep.CreateCategory(category);//instead of hardcoding  we can do automapper like this
                    return CreatedAtAction("AddVisitorcategory", new { id = category.VisitorCategoryId }, category);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch(Exception)
              {
                return StatusCode(StatusCodes.Status500InternalServerError,
           "Error creating new visitorcategory record record");
              }

        }

        [HttpPut]
        
        public async Task<IActionResult> UpdateCategorydetails(UpdateCategorDTO updateCategoryRequest)
        {
           
            try
            {
                _logger.LogInformation("Executing {Action}", nameof(UpdateCategorydetails));
                if (updateCategoryRequest == null)
                {
                    _logger.LogError($"Error while try to update category record");
                    return BadRequest();
                }
                var category = _mapper.Map<VisitorCategoryDetails>(updateCategoryRequest);
                await _serviceRep.UpdateCategory(category);
                return NoContent();
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error updating category details data");
            }
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteCategorydetails(int id)
        {
            try
            {
                _logger.LogInformation("Executing {Action} with id: {id}", nameof(DeleteCategorydetails), id);
                if (id == 0)
                {
                    _logger.LogError($"Error while try to delete category record list");
                    return BadRequest();
                }
                var categoryDetails = await _serviceRep.finddata(id);

                await _serviceRep.DeleteCategory(categoryDetails);
                return NoContent();
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error DeleteCategorydetails data");
            }
        }
    }
}

