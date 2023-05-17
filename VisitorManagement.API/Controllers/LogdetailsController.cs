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
        public async Task<IActionResult>GetVisitorLogs()
        {
            try
            {
                _logger.LogInformation("Executing {Action}", nameof(GetVisitorLogs));
                var visitorLog = await _visitorLogRepo.GetAllVisitorLogs();
                var visitorLogDto = _mapper.Map<List<GetLogsDTO>>(visitorLog);
                if (visitorLogDto == null)
                {
                    _logger.LogError($"Error while try to get VisitorLog records");
                    return NoContent();
                }
                return Ok(visitorLogDto);
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                    "Error retrieving visitor log from the database based on Id");
            }
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetVisitorLogById(int id)
        {
            try
            {
                _logger.LogInformation("Executing {Action} with id: {id}", nameof(GetVisitorLogById), id);
                var visitorLogById = await _visitorLogRepo.GetVisitorLogById(id);
                if (visitorLogById == null)
                {
                    _logger.LogError($"Error while try to get visitorlog by id record");
                    return NoContent();

                }
                var visitorLogDto = _mapper.Map<GetLogsDTO>(visitorLogById);
                return Ok(visitorLogDto);
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving visitor log from the database based on Id");
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddVisitorLogs([FromBody] CreateLogDTO addvisitorslog)
        {
            try
            {
                if (ModelState.IsValid)
                {


                    _logger.LogInformation("Executing {Action} ", nameof(AddVisitorLogs));
                    var addLogs = _mapper.Map<VisitorLogsDetails>(addvisitorslog);
                    await _visitorLogRepo.CreateVisitorLog(addLogs);
                    return CreatedAtAction("AddVisitorLogs", new { id = addLogs.VisitorLogId }, addLogs);
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

        public async Task<IActionResult> UpdateVisitorLog(UpdateDTO updatelog)
        {
            try
            {
                _logger.LogInformation("Executing {Action} ", nameof(UpdateVisitorLog));
                if (updatelog == null)
                {
                    return BadRequest();
                }
                var updatelogs = _mapper.Map<VisitorLogsDetails>(updatelog);
                await _visitorLogRepo.UpdateVisitorLog(updatelogs);
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
                var logById = await _visitorLogRepo.GetVisitorLogById(id);
                await _visitorLogRepo.DeleteVisitoryLog(logById);
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
