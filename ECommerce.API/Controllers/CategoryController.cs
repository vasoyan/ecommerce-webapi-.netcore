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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryServices;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ICategoryService categoryServices, ILogger<CategoryController> logger)
        {
            _categoryServices = categoryServices ?? throw new ArgumentNullException(nameof(categoryServices));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryVM>>> GetAllByFilterWithPagedList(int pageIndex = 1, int pageSize = 20)
        {
            try
            {
                var result = await _categoryServices.GetFilteredPagedListAsync(pageIndex, pageSize);

                if (result.Any())
                    return Ok(new ApiResponse<IEnumerable<CategoryVM>>(result, (int)HttpStatusCode.OK));
                else
                    return NotFound(new ApiResponse<CategoryVM>(Constants.DATA_NOT_FOUND_MESSAGE, (int)HttpStatusCode.NotFound));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Format(Constants.EXCEPTION_MESSAGE, ex?.InnerException));
                return StatusCode(500, new ApiResponse<CategoryVM>(ex, (int)HttpStatusCode.InternalServerError));
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CategoryVM>> GetById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogInformation(Constants.INVALID_USERID_MESSAGE);
                    return BadRequest(new ApiResponse<CategoryVM>(Constants.INVALID_USERID_MESSAGE, (int)HttpStatusCode.BadRequest));
                }

                var result = await _categoryServices.GetByIdAsync(id);

                if (result != null)
                    return Ok(new ApiResponse<CategoryVM>(result, (int)HttpStatusCode.OK));
                else
                    return NotFound(new ApiResponse<CategoryVM>(Constants.DATA_NOT_FOUND_MESSAGE, (int)HttpStatusCode.NotFound));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Format(Constants.EXCEPTION_MESSAGE, ex?.InnerException));
                return StatusCode(500, new ApiResponse<CategoryVM>(ex, (int)HttpStatusCode.InternalServerError));
            }
        }

        [HttpPost("save")]
        public async Task<ActionResult<CategoryVM>> SaveAsync(CategoryDTO categoryDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogInformation(Constants.MODEL_STATE_NOT_VALID);
                    return BadRequest(new ApiResponse<object>(Constants.MODEL_STATE_NOT_VALID, (int)HttpStatusCode.BadRequest));
                }

                var result = await _categoryServices.SaveAsync(categoryDTO);

                if (result != null)
                    return Ok(new ApiResponse<CategoryVM>(result, (int)HttpStatusCode.OK));
                else
                    return NotFound(new ApiResponse<CategoryVM>(Constants.DATA_NOT_FOUND_MESSAGE, (int)HttpStatusCode.NotFound));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Format(Constants.EXCEPTION_MESSAGE, ex?.InnerException));
                return StatusCode(500, new ApiResponse<CategoryVM>(ex, (int)HttpStatusCode.InternalServerError));
            }
        }

        [HttpPut("update/{id:int}")]
        public async Task<ActionResult<CategoryVM>> UpdateAsync(int id, CategoryDTO categoryDTO)
        {
            try
            {
                if (!ModelState.IsValid || categoryDTO.Id <= 0 || id <= 0)
                {
                    _logger.LogInformation(Constants.MODEL_STATE_NOT_VALID);
                    return BadRequest(new ApiResponse<object>(Constants.MODEL_STATE_NOT_VALID, (int)HttpStatusCode.BadRequest));
                }

                var categoryData = await _categoryServices.GetByIdAsync(id);
                if (categoryData == null)
                    return NotFound(new ApiResponse<CategoryVM>(Constants.DATA_NOT_FOUND_MESSAGE, (int)HttpStatusCode.NotFound));

                var result = await _categoryServices.UpdateAsync(categoryDTO);
                if (result != null)
                    return Ok(new ApiResponse<CategoryVM>(result, (int)HttpStatusCode.OK));
                else
                    return NotFound(new ApiResponse<CategoryVM>(Constants.DATA_NOT_FOUND_MESSAGE, (int)HttpStatusCode.NotFound));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Format(Constants.EXCEPTION_MESSAGE, ex?.InnerException));
                return StatusCode(500, new ApiResponse<CategoryVM>(ex, (int)HttpStatusCode.InternalServerError));
            }
        }

        [HttpDelete("delete/{id:int}")]
        public async Task<ActionResult<CategoryVM>> DeleteAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogInformation(Constants.INVALID_USERID_MESSAGE);
                    return BadRequest(new ApiResponse<CategoryVM>(Constants.INVALID_USERID_MESSAGE, (int)HttpStatusCode.BadRequest));
                }

                var result = await _categoryServices.DeleteAsync(id);
                if (result)
                    return Ok(new ApiResponse<string>(Constants.DELETE_MESSAGE, (int)HttpStatusCode.OK));
                else
                    return NotFound(new ApiResponse<UserVM>(Constants.DATA_NOT_FOUND_MESSAGE, (int)HttpStatusCode.NotFound));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Format(Constants.EXCEPTION_MESSAGE, ex?.InnerException));
                return StatusCode(500, new ApiResponse<CategoryVM>(ex, (int)HttpStatusCode.InternalServerError));
            }
        }
    }
}
