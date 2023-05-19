using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VisitorManagement.API.DTO.CategoryDTO;
using VisitorManagement.API.InputModels;
using VisitorManagement.Models;
using VisitorManagement.Service.IRepoInfo;

namespace VisitorManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogDetailsController : ControllerBase
    {
        private readonly IVisitorLogRepository _visitorLogSerrvice;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IPaginationServices<CreateLogDTO, VisitorLogsDetails> _paginationServices;
        public LogDetailsController(IVisitorLogRepository visitorLogRepo,IMapper mapper,ILogger<LogDetailsController> logger,IPaginationServices<CreateLogDTO,VisitorLogsDetails> pagination)
        {
            _visitorLogSerrvice = visitorLogRepo;
            _mapper = mapper;
            _logger = logger;
            _paginationServices = pagination;

        }
        #region Get Visitior Log details
        [HttpGet]
        public async Task<IActionResult>GetVisitorLogs()
        {
            try
            {
                _logger.LogInformation("Executing {Action}", nameof(GetVisitorLogs));
                var visitorLog = await _visitorLogSerrvice.GetAllVisitorLogs();   
                var visitorLogDto = _mapper.Map<List<GetLogsDTO>>(visitorLog);  //Mapping Dto model
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
        #endregion

        #region Get Visitor Log By Id
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetVisitorLogById(int id)
        {
            try
            {
                _logger.LogInformation("Executing {Action} with id: {id}", nameof(GetVisitorLogById), id);
                var visitorLogById = await _visitorLogSerrvice.GetVisitorLogById(id);
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
        #endregion

        #region Create New Visitor Log

        [HttpPost]
        public async Task<IActionResult> AddVisitorLogs([FromBody] CreateLogDTO addvisitorslog)
        {
            try
            {
                if (ModelState.IsValid)
                {


                    _logger.LogInformation("Executing {Action} ", nameof(AddVisitorLogs));
                    var addLogs = _mapper.Map<VisitorLogsDetails>(addvisitorslog);
                    await _visitorLogSerrvice.CreateVisitorLog(addLogs);
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
        #endregion

        #region Updating Existing Visitor Log
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
                await _visitorLogSerrvice.UpdateVisitorLog(updatelogs);
                return NoContent();
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error updating visitor log details data");
            }
        }
        #endregion

        #region Delete Visitor Log By Id
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
                var logById = await _visitorLogSerrvice.GetVisitorLogById(id);
                await _visitorLogSerrvice.DeleteVisitoryLog(logById);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                    "Error DeleteVisitorlogs data");
            }
        }
        #endregion

        #region Pagination for Visitior Logs

        [HttpPost]
        [Route("GetPagination")]
        public async Task<IActionResult> GetPaginationForLogs([FromBody]PaginationInput paginationInput)
        {
            var visitorLogs = await _visitorLogSerrvice.GetAllVisitorLogs();
            var result =  _paginationServices.GetPagination(visitorLogs, paginationInput);
            return Ok(result);
        }
        #endregion
    }
}
