using ECommerce.API.Common;
using ECommerce.Application.IServices;
using ECommerce.Application.Models.DTOs;
using ECommerce.Application.Models.Helpers;
using ECommerce.Application.Models.VMs;
using ECommerce.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleServices;
        private readonly ILogger<RoleController> _logger;

        public RoleController(IRoleService roleServices, ILogger<RoleController> logger)
        {
            _roleServices = roleServices ?? throw new ArgumentNullException(nameof(roleServices));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleVM>>> GetAllByFilterWithPagedList(int pageIndex = 1, int pageSize = 20)
        {
            try
            {
                var result = await _roleServices.GetFilteredPagedListAsync(pageIndex, pageSize);

                if (result.Any())
                    return Ok(new ApiResponse<IEnumerable<RoleVM>>(result, (int)HttpStatusCode.OK));
                else
                    return NotFound(new ApiResponse<RoleVM>(Constants.DATA_NOT_FOUND_MESSAGE, (int)HttpStatusCode.NotFound));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Format(Constants.EXCEPTION_MESSAGE, ex?.InnerException));
                return StatusCode(500, new ApiResponse<RoleVM>(ex, (int)HttpStatusCode.InternalServerError));
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<RoleVM>> GetById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogInformation(Constants.INVALID_USERID_MESSAGE);
                    return BadRequest(new ApiResponse<RoleVM>(Constants.INVALID_USERID_MESSAGE, (int)HttpStatusCode.BadRequest));
                }

                var result = await _roleServices.GetByIdAsync(id);

                if (result != null)
                    return Ok(new ApiResponse<RoleVM>(result, (int)HttpStatusCode.OK));
                else
                    return NotFound(new ApiResponse<RoleVM>(Constants.DATA_NOT_FOUND_MESSAGE, (int)HttpStatusCode.NotFound));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Format(Constants.EXCEPTION_MESSAGE, ex?.InnerException));
                return StatusCode(500, new ApiResponse<RoleVM>(ex, (int)HttpStatusCode.InternalServerError));
            }
        }

        [HttpPost("save")]
        public async Task<ActionResult<RoleVM>> SaveAsync(RoleDTO roleDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogInformation(Constants.MODEL_STATE_NOT_VALID);
                    return BadRequest(new ApiResponse<object>(Constants.MODEL_STATE_NOT_VALID, (int)HttpStatusCode.BadRequest));
                }

                var result = await _roleServices.SaveAsync(roleDTO);

                if (result != null)
                    return Ok(new ApiResponse<RoleVM>(result, (int)HttpStatusCode.OK));
                else
                    return NotFound(new ApiResponse<RoleVM>(Constants.DATA_NOT_FOUND_MESSAGE, (int)HttpStatusCode.NotFound));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{Constants.EXCEPTION_MESSAGE} - {ex?.InnerException}");
                return StatusCode(500, new ApiResponse<RoleVM>(ex, (int)HttpStatusCode.InternalServerError));
            }
        }

        [HttpPut("update/{id:int}")]
        public async Task<ActionResult<RoleVM>> UpdateAsync(int id, RoleDTO roleDTO)
        {
            try
            {
                if (!ModelState.IsValid || roleDTO.Id <= 0 || id <= 0)
                {
                    _logger.LogInformation(Constants.MODEL_STATE_NOT_VALID);
                    return BadRequest(new ApiResponse<object>(Constants.MODEL_STATE_NOT_VALID, (int)HttpStatusCode.BadRequest));
                }

                var roleData = await _roleServices.GetByIdAsync(id);
                if (roleData == null)
                    return NotFound(new ApiResponse<RoleVM>(Constants.DATA_NOT_FOUND_MESSAGE, (int)HttpStatusCode.NotFound));

                var result = await _roleServices.UpdateAsync(roleDTO);
                if (result != null)
                    return Ok(new ApiResponse<RoleVM>(result, (int)HttpStatusCode.OK));
                else
                    return NotFound(new ApiResponse<RoleVM>(Constants.DATA_NOT_FOUND_MESSAGE, (int)HttpStatusCode.NotFound));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Format(Constants.EXCEPTION_MESSAGE, ex?.InnerException));
                return StatusCode(500, new ApiResponse<RoleVM>(ex, (int)HttpStatusCode.InternalServerError));
            }
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<RoleVM>> DeleteAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogInformation(Constants.INVALID_USERID_MESSAGE);
                    return BadRequest(new ApiResponse<RoleVM>(Constants.INVALID_USERID_MESSAGE, (int)HttpStatusCode.BadRequest));
                }

                var result = await _roleServices.DeleteAsync(id);
                if (result)
                    return Ok(new ApiResponse<string>(Constants.DELETE_MESSAGE, (int)HttpStatusCode.OK));
                else
                    return NotFound(new ApiResponse<UserVM>(Constants.DATA_NOT_FOUND_MESSAGE, (int)HttpStatusCode.NotFound));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Format(Constants.EXCEPTION_MESSAGE, ex?.InnerException));
                return StatusCode(500, new ApiResponse<RoleVM>(ex, (int)HttpStatusCode.InternalServerError));
            }
        }
    }
}
