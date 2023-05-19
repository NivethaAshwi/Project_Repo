using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;
using System.Text.Json;
using VisitorManagement.API.DTO;
using VisitorManagement.API.DTO.CategoryDTO;
using VisitorManagement.API.InputModels;
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

        private readonly IVisitorRepository _visitorService;
        private readonly IMapper _mapper;
        private readonly ILogger<VisitorController> _logger;
        private readonly IPaginationServices<CreateVisitorDTO, VisitorDetails> _paginationServices;

        public VisitorController(IVisitorRepository visitorRepo, IMapper mapper, ILogger<VisitorController> logger, IPaginationServices<CreateVisitorDTO, VisitorDetails> paginationServices)
        {
            _visitorService = visitorRepo;
            _mapper = mapper;
            _logger = logger;
            _paginationServices = paginationServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetVisitorDetails()
        {
            try
            {
                _logger.LogInformation("Executing {Action}", nameof(GetVisitorDetails));
                var visitor = await _visitorService.GetAllVisitors();
                var visitorDto = _mapper.Map<List<GetVisitorDTO>>(visitor);
                if (visitor == null)
                {
                    _logger.LogError($"Error while try to get visitor details records");
                    return NoContent();
                }
                return Ok(visitorDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                    "Error retrieving data from the database");
            }
        }


        [HttpPost]
        public async Task<IActionResult> AddVisitors([FromBody] CreateVisitorDTO addvisitorRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.LogInformation("Executing {Action}", nameof(AddVisitors));
                    var addVisitor = _mapper.Map<VisitorDetails>(addvisitorRequest);
                    await _visitorService.CreateVisitor(addVisitor);
                    return CreatedAtAction("AddVisitors", new { id = addVisitor.VisitorId }, addVisitor);
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
        public async Task<IActionResult> UpdateVisitorsDetails(UpdateVisitorDTO updateVisitor)
        {
            try
            {
                _logger.LogInformation("Executing {Action}", nameof(UpdateVisitorsDetails));
                if (updateVisitor == null)
                {
                    return BadRequest();
                }
                var visitors = _mapper.Map<VisitorDetails>(updateVisitor);
                await _visitorService.UpdateVisitor(visitors);
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
        public async Task<IActionResult> DeleteVisitorById(int id)
        {
            try
            {
                _logger.LogInformation("Executing {Action} with id: {id}", nameof(DeleteVisitorById), id);
                if (id == 0)
                {
                    return BadRequest();
                }
                var visitors = await _visitorService.FindVisitorById(id);
                await _visitorService.DeleteVisitor(visitors);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error DeleteResident data");
            }

        }
        [HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<GetVisitorDTO>>> SearchVisitors(string MobileNumber, string VehicleNO)
        {
            try
            {
                var result = await _visitorService.SearchByVisitorDetails(MobileNumber, VehicleNO);

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
        [HttpPost]
        [Route("GetVisitordetailPagination")]
        public async Task<ActionResult> GetPaginationDetailsforVisitors([FromBody]PaginationInput paginationInput)
        {
            var visitorDetails = await _visitorService.GetAllVisitors();
            var result = _paginationServices.GetPagination(visitorDetails, paginationInput);
            return Ok(result);


        }
      
    }
}
