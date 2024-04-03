using ECommerce.API.Common;
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
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _permissionServices;
        private readonly ILogger<PermissionController> _logger;

        public PermissionController(IPermissionService permissionServices, ILogger<PermissionController> logger)
        {
            _permissionServices = permissionServices ?? throw new ArgumentNullException(nameof(permissionServices));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PermissionVM>>> GetAllByFilterWithPagedList(int pageIndex = 1, int pageSize = 20)
        {
            try
            {
                var result = await _permissionServices.GetFilteredPagedListAsync(pageIndex, pageSize);

                if (result.Any())
                    return Ok(new ApiResponse<IEnumerable<PermissionVM>>(result, (int)HttpStatusCode.OK));
                else
                    return NotFound(new ApiResponse<PermissionVM>(Constants.DATA_NOT_FOUND_MESSAGE, (int)HttpStatusCode.NotFound));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Format(Constants.EXCEPTION_MESSAGE, ex?.InnerException));
                return StatusCode(500, new ApiResponse<PermissionVM>(ex, (int)HttpStatusCode.InternalServerError));
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<PermissionVM>> GetById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogInformation(Constants.INVALID_USERID_MESSAGE);
                    return BadRequest(new ApiResponse<PermissionVM>(Constants.INVALID_USERID_MESSAGE, (int)HttpStatusCode.BadRequest));
                }

                var result = await _permissionServices.GetByIdAsync(id);

                if (result != null)
                    return Ok(new ApiResponse<PermissionVM>(result, (int)HttpStatusCode.OK));
                else
                    return NotFound(new ApiResponse<PermissionVM>(Constants.DATA_NOT_FOUND_MESSAGE, (int)HttpStatusCode.NotFound));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Format(Constants.EXCEPTION_MESSAGE, ex?.InnerException));
                return StatusCode(500, new ApiResponse<PermissionVM>(ex, (int)HttpStatusCode.InternalServerError));
            }
        }

        [HttpPost("save")]
        public async Task<ActionResult<PermissionVM>> SaveAsync(PermissionDTO permissionDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogInformation(Constants.MODEL_STATE_NOT_VALID);
                    return BadRequest(new ApiResponse<object>(Constants.MODEL_STATE_NOT_VALID, (int)HttpStatusCode.BadRequest));
                }

                var result = await _permissionServices.SaveAsync(permissionDTO);

                if (result != null)
                    return Ok(new ApiResponse<PermissionVM>(result, (int)HttpStatusCode.OK));
                else
                    return NotFound(new ApiResponse<PermissionVM>(Constants.DATA_NOT_FOUND_MESSAGE, (int)HttpStatusCode.NotFound));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Format(Constants.EXCEPTION_MESSAGE, ex?.InnerException));
                return StatusCode(500, new ApiResponse<PermissionVM>(ex, (int)HttpStatusCode.InternalServerError));
            }
        }

        [HttpPut("update/{id:int}")]
        public async Task<ActionResult<PermissionVM>> UpdateAsync(int id, PermissionDTO permissionDTO)
        {
            try
            {
                if (!ModelState.IsValid || permissionDTO.Id <= 0 || id <= 0)
                {
                    _logger.LogInformation(Constants.MODEL_STATE_NOT_VALID);
                    return BadRequest(new ApiResponse<object>(Constants.MODEL_STATE_NOT_VALID, (int)HttpStatusCode.BadRequest));
                }

                var permissionData = await _permissionServices.GetByIdAsync(id);
                if (permissionData == null)
                    return NotFound(new ApiResponse<PermissionVM>(Constants.DATA_NOT_FOUND_MESSAGE, (int)HttpStatusCode.NotFound));

                var result = await _permissionServices.UpdateAsync(permissionDTO);
                if (result != null)
                    return Ok(new ApiResponse<PermissionVM>(result, (int)HttpStatusCode.OK));
                else
                    return NotFound(new ApiResponse<PermissionVM>(Constants.DATA_NOT_FOUND_MESSAGE, (int)HttpStatusCode.NotFound));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Format(Constants.EXCEPTION_MESSAGE, ex?.InnerException));
                return StatusCode(500, new ApiResponse<PermissionVM>(ex, (int)HttpStatusCode.InternalServerError));
            }
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<PermissionVM>> DeleteAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogInformation(Constants.INVALID_USERID_MESSAGE);
                    return BadRequest(new ApiResponse<PermissionVM>(Constants.INVALID_USERID_MESSAGE, (int)HttpStatusCode.BadRequest));
                }

                var result = await _permissionServices.DeleteAsync(id);
                if (result)
                    return Ok(new ApiResponse<string>(Constants.DELETE_MESSAGE, (int)HttpStatusCode.OK));
                else
                    return NotFound(new ApiResponse<UserVM>(Constants.DATA_NOT_FOUND_MESSAGE, (int)HttpStatusCode.NotFound));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Format(Constants.EXCEPTION_MESSAGE, ex?.InnerException));
                return StatusCode(500, new ApiResponse<PermissionVM>(ex, (int)HttpStatusCode.InternalServerError));
            }
        }
    }
}
