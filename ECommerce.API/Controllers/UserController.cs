using ECommerce.Application.IServices;
using ECommerce.Application.Models.DTOs;
using ECommerce.Application.Models.Helpers;
using ECommerce.Application.Models.VMs;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private const string INVALID_USERID_MESSAGE = "Id should be greater than 0.";
        private const string DATA_NOT_FOUND_MESSAGE = "Data not found.";
        private const string EXCEPTION_MESSAGE = "An error occurred while processing the request. Exception: ";

        private readonly IUserService _userServices;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userServices, ILogger<UserController> logger)
        {
            _userServices = userServices ?? throw new ArgumentNullException(nameof(userServices));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("paged-lists")]
        public async Task<ActionResult<IEnumerable<UserVM>>> GetAllByFilterWithPagedList(int pageIndex = 1, int pageSize = 20)
        {
            try
            {
                var result = await _userServices.GetFilteredPagedListAsync(pageIndex, pageSize);

                if (result.Any())
                    return Ok(new ApiResponse<IEnumerable<UserVM>>(result, (int)HttpStatusCode.OK));
                else
                    return NotFound(new ApiResponse<UserVM>(DATA_NOT_FOUND_MESSAGE, (int)HttpStatusCode.NotFound));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{EXCEPTION_MESSAGE} - {ex}");
                return StatusCode(500, new ApiResponse<UserVM>(ex, (int)HttpStatusCode.InternalServerError));
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserVM>> GetById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogInformation(INVALID_USERID_MESSAGE);
                    return BadRequest(new ApiResponse<UserVM>(INVALID_USERID_MESSAGE, (int)HttpStatusCode.BadRequest));
                }

                var result = await _userServices.GetByIdAsync(id);

                if (result != null)
                    return Ok(new ApiResponse<UserVM>(result, (int)HttpStatusCode.OK));
                else
                    return NotFound(new ApiResponse<UserVM>(DATA_NOT_FOUND_MESSAGE, (int)HttpStatusCode.NotFound));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{EXCEPTION_MESSAGE} - {ex}");
                return StatusCode(500, new ApiResponse<UserVM>(ex, (int)HttpStatusCode.InternalServerError));
            }
        }

        [HttpPost("save")]
        public async Task<ActionResult<UserVM>> SaveAsync(UserDTO userDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogInformation($"Model User not validated for {ModelState}");
                    return BadRequest(new ApiResponse<object>(ModelState, (int)HttpStatusCode.BadRequest));
                }

                var result = await _userServices.SaveAsync(userDTO);

                if (result != null)
                    return Ok(new ApiResponse<UserVM>(result, (int)HttpStatusCode.OK));
                else
                    return NotFound(new ApiResponse<UserVM>(DATA_NOT_FOUND_MESSAGE, (int)HttpStatusCode.NotFound));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{EXCEPTION_MESSAGE} - {ex}");
                return StatusCode(500, new ApiResponse<UserVM>(ex, (int)HttpStatusCode.InternalServerError));
            }
        }

        [HttpPut("update")]
        public async Task<ActionResult<UserVM>> UpdateAsync(UserDTO userDTO)
        {
            try
            {
                if (!ModelState.IsValid || userDTO.Id == 0)
                {
                    _logger.LogInformation($"ModelUser not validated for {ModelState} or userDTO.Id is 0");
                    return BadRequest(new ApiResponse<object>(ModelState, (int)HttpStatusCode.BadRequest));
                }

                var result = await _userServices.UpdateAsync(userDTO);
                if (result != null)
                    return Ok(new ApiResponse<UserVM>(result, (int)HttpStatusCode.OK));
                else
                    return NotFound(new ApiResponse<UserVM>(DATA_NOT_FOUND_MESSAGE, (int)HttpStatusCode.NotFound));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{EXCEPTION_MESSAGE} - {ex}");
                return StatusCode(500, new ApiResponse<UserVM>(ex, (int)HttpStatusCode.InternalServerError));
            }
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<UserVM>> DeleteAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogInformation(INVALID_USERID_MESSAGE);
                    return BadRequest(new ApiResponse<UserVM>(INVALID_USERID_MESSAGE, (int)HttpStatusCode.BadRequest));
                }

                await _userServices.DeleteAsync(id);

                _logger.LogInformation($"Data with ID {id} deleted successfully.");
                return Ok(new ApiResponse<string>("Data deleted successfully.", (int)HttpStatusCode.OK));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{EXCEPTION_MESSAGE} - {ex}");
                return StatusCode(500, new ApiResponse<UserVM>(ex, (int)HttpStatusCode.InternalServerError));
            }
        }
    }
}
