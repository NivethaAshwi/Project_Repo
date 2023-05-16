using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VisitorManagement.API.DTO.CategoryDTO;
using VisitorManagement.Models;
using VisitorManagement.Service.IRepoInfo;

namespace VisitorManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogdetailsController : ControllerBase
    {
        private readonly IVisitorLogRepo _visitorLogRepo;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public LogdetailsController(IVisitorLogRepo visitorLogRepo,IMapper mapper,ILogger<LogdetailsController> logger)
        {
            _visitorLogRepo = visitorLogRepo;
            _mapper = mapper;
            _logger = logger;

        }
        [HttpGet]
        public async Task<IActionResult>GetVisitorLogdetails()
        {
            try
            {
                _logger.LogInformation("Executing {Action}", nameof(GetVisitorLogdetails));
                var visitorlogs = await _visitorLogRepo.GetAllVisitorLogs();
                var visitorlogdto = _mapper.Map<List<GetLogsDTO>>(visitorlogs);
                if (visitorlogdto == null)
                {
                    _logger.LogError($"Error while try to get VisitorLog records");
                    return NoContent();
                }
                return Ok(visitorlogdto);
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                    "Error retrieving visitor log from the database based on Id");
            }
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetVisitorlogbyId(int id)
        {
            try
            {
                _logger.LogInformation("Executing {Action} with id: {id}", nameof(GetVisitorlogbyId), id);
                var visitorlogbyid = await _visitorLogRepo.GetVisitorlogbyid(id);
                if (visitorlogbyid == null)
                {
                    _logger.LogError($"Error while try to get visitorlog by id record");
                    return NoContent();

                }
                var visitorlogdsto = _mapper.Map<GetLogsDTO>(visitorlogbyid);
                return Ok(visitorlogdsto);
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving visitor log from the database based on Id");
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddVisitorlogs([FromBody] CreateLogDTO addvisitorslog)
        {
            try
            {
                if (ModelState.IsValid)
                {


                    _logger.LogInformation("Executing {Action} ", nameof(AddVisitorlogs));
                    var Addlogs = _mapper.Map<VisitorLogsDetails>(addvisitorslog);
                    await _visitorLogRepo.CreateVisitorlog(Addlogs);
                    return CreatedAtAction("AddVisitorlogs", new { id = Addlogs.VisitorLogId }, Addlogs);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
              "Error creating new visitor log record");
            }
        }
        [HttpPut]

        public async Task<IActionResult> UpdateVisitorlogdetails(UpdateDTO updatelogRequest)
        {
            try
            {
                _logger.LogInformation("Executing {Action} ", nameof(UpdateVisitorlogdetails));
                if (updatelogRequest == null)
                {
                    return BadRequest();
                }
                var updatelogs = _mapper.Map<VisitorLogsDetails>(updatelogRequest);
                await _visitorLogRepo.Updatevisitorlog(updatelogs);
                return NoContent();
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error updating visitor log details data");
            }
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteVisitorlogs(int id)
        {
            try
            {
                _logger.LogInformation("Executing {Action} with id: {id}", nameof(DeleteVisitorlogs), id);
                if (id == 0)
                {
                    return BadRequest();
                }
                var dellogbyid = await _visitorLogRepo.GetVisitorlogbyid(id);
                await _visitorLogRepo.Deletevisitorylog(dellogbyid
                    );
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                    "Error DeleteVisitorlogs data");
            }
        }

    }
}
