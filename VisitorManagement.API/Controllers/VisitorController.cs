using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using VisitorManagement.API.DTO;
using VisitorManagement.API.DTO.CategoryDTO;

using VisitorManagement.DataAccess;
using VisitorManagement.Models;
using VisitorManagement.Service.IRepoInfo;

namespace VisitorManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitorController : ControllerBase
    {
        //private readonly VisitorCategoryDBContext _Dbcontext;
      
        private readonly IVisitorRepo _visitorserRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<VisitorController> _logger;

        public VisitorController(IVisitorRepo visitorRepo,IMapper mapper,ILogger<VisitorController> logger)
        {
            _visitorserRepo = visitorRepo;
            _mapper = mapper;
            _logger = logger;
        }
       
        [HttpGet]
        public async Task<IActionResult> GetVisitorDetails()
        {
            try
            {
                _logger.LogInformation("Executing {Action}", nameof(GetVisitorDetails));
                var visitor = await _visitorserRepo.GetAllVisitors();
                var visitordto = _mapper.Map<List<GetVisitorDTO>>(visitor);
                if (visitor == null)
                {
                    _logger.LogError($"Error while try to get visitor details records");
                    return NoContent();
                }
                return Ok(visitordto);
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                    "Error retrieving data from the database");
            }
        }
       
        
        [HttpPost]
        public async Task<IActionResult> AddVisitordetails([FromBody] CreateVisitorDTO addvisitorRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.LogInformation("Executing {Action}", nameof(AddVisitordetails));
                    var addvisitor = _mapper.Map<Visitordetails>(addvisitorRequest);
                    await _visitorserRepo.CreateVisitor(addvisitor);
                    return CreatedAtAction("AddVisitordetails", new { id = addvisitor.VisitorId }, addvisitor);
                }
                else

                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
           "Error creating new visitorcategory record record");
            }

        }
        [HttpPut]
      public async Task<IActionResult> UpdateVisitordetails( UpdateVisitorDTO updateContactRequest)
        {
            try
            {
                _logger.LogInformation("Executing {Action}", nameof(UpdateVisitordetails));
                if (updateContactRequest == null)
                {
                    return BadRequest();
                }
                var visitors = _mapper.Map<Visitordetails>(updateContactRequest);
                await _visitorserRepo.Updatevisitor(visitors);
                return NoContent();

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error updating visitor details data");
            }
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteVisitordetails(int id)
        {
            try
            {
                _logger.LogInformation("Executing {Action} with id: {id}", nameof(DeleteVisitordetails), id);
                if (id == 0)
                {
                    return BadRequest();
                }
                var contact = await _visitorserRepo.Fetchdata(id);
                await _visitorserRepo.Deletevisitory(contact);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error DeleteResident data");
            }

        }
        [HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<GetVisitorDTO>>> search(string MobileNumber,string VehicleNO)
        {
            try
            {
                var result = await _visitorserRepo.Searchbyvisitor(MobileNumber, VehicleNO);

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
