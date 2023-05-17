using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.Metrics;
using VisitorManagement.DataAccess;
using VisitorManagement.Models;
using AutoMapper;
using System.Reflection;
using VisitorManagement.Service.IRepoInfo;
using VisitorManagement.API.DTO.CategoryDTO;
using VisitorManagement.API.DTO;
using System.Text.Json;

namespace VisitorManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResidentController : ControllerBase
    {
        
        private readonly IResidentRenpo _residentService;
        private readonly IMapper _mapper;
        private readonly ILogger<VisitorCategoryController> _logger;
        public ResidentController(IResidentRenpo resident,IMapper mapper,ILogger<VisitorCategoryController> logger)
        {
            _residentService = resident;
            _mapper = mapper;
            _logger = logger ;

        }
        [HttpGet]
        public async Task<IActionResult> GetResident()
        {
            try
            {
                _logger.LogInformation("Executing {Action}", nameof(GetResident));
                var residents = await _residentService.GetAllResident();
                var residentDTo = _mapper.Map<List<GetResidentDTO>>(residents);
                if (residentDTo == null)
                {
                    _logger.LogError($"Error while try to get Resident records");
                    return NoContent();

                }
                return Ok(residentDTo);
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                    "Error retrieving data from the database");
            }
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetResidentById(int id)
        {
            try
            {
                _logger.LogInformation("Executing {Action} with id: {id}", nameof(GetResidentById), id);
                var residentById = await _residentService.GetResidentById(id);
                if (residentById == null)
                {
                    _logger.LogError($"Error while try to get Resident by id record");
                    return NoContent();

                }
                var Residentiddto = _mapper.Map<List<GetResidentDTO>>(residentById);
                return Ok(Residentiddto);
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database based on Id");
            }
        }
        [HttpPost]
        public async Task<IActionResult> InsertResidentDetails([FromBody]CreateResidentDTO addResidentDetails)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    _logger.LogInformation("Executing {Action}", nameof(InsertResidentDetails));
                    var addResident = _mapper.Map<ResidentDetails>(addResidentDetails);
                    await _residentService.CreateResident(addResident);
                    return CreatedAtAction("InsertResidentdetails", new { id = addResident.ResidentId }, addResident);
                }
                else
                {
                      return BadRequest(ModelState);
                   
                }
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error creating new Residentrecord record");
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateResident(UpdateResidentDTO updateResidentRequest)
        {
            try
            {
                _logger.LogInformation("Executing {Action}", nameof(UpdateResident));
                if (updateResidentRequest == null)
                {
                    return BadRequest();
                }
                var updateResident = _mapper.Map<ResidentDetails>(updateResidentRequest);
                await _residentService.UpdateResident(updateResident);
                return NoContent();
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error updating restident details data");
            }
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteResidentById(int id)
        {
            try
            {
                _logger.LogInformation("Executing {Action} with id: {id}", nameof(DeleteResidentById), id);
                if (id == 0)
                {
                    return BadRequest();
                }
                var removeById = await _residentService.FindResidentsInfo(id);

                await _residentService.DeleteResident(removeById);
                return NoContent();
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error DeleteResident data");
            }
        }
        //) Search resident by flat number or name (first name, last name or full name).
        [HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<GetResidentDTO>>> SearchResidentDetails(string FlatNumber, string FirstName, string LastName)
        {
            try
            {
                var result = await _residentService.SearchByDetails(FlatNumber, FirstName,LastName);

                if (result.Any())
                {
                    return Ok(result);
                }

                
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

    }

}
