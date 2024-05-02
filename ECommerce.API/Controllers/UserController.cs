using ECommerce.API.Common;
using ECommerce.Application.IServices;
using ECommerce.Application.Models.DTOs;
using ECommerce.Application.Models.Helpers;
using ECommerce.Application.Models.VMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userServices, ITokenService tokenService, ILogger<UserController> logger) : ControllerBase
    {
        private readonly IUserService _userServices = userServices ?? throw new ArgumentNullException(nameof(userServices));
        private readonly ITokenService _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        private readonly ILogger<UserController> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserVM>>> GetAllByFilterWithPagedList(int pageIndex = 1, int pageSize = 20)
        {
            try
            {
                var result = await _userServices.GetFilteredPagedListAsync(pageIndex, pageSize);

                if (result.Any())
                    return Ok(new ApiResponse<IEnumerable<UserVM>>(result, (int)HttpStatusCode.OK));
                else
                    return NotFound(new ApiResponse<UserVM>(Constants.DATA_NOT_FOUND_MESSAGE, (int)HttpStatusCode.NotFound));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Format(Constants.EXCEPTION_MESSAGE, ex?.InnerException));
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
                    _logger.LogInformation(Constants.INVALID_USERID_MESSAGE);
                    return BadRequest(new ApiResponse<UserVM>(Constants.INVALID_USERID_MESSAGE, (int)HttpStatusCode.BadRequest));
                }

                var result = await _userServices.GetByIdAsync(id);

                if (result != null)
                    return Ok(new ApiResponse<UserVM>(result, (int)HttpStatusCode.OK));
                else
                    return NotFound(new ApiResponse<UserVM>(Constants.DATA_NOT_FOUND_MESSAGE, (int)HttpStatusCode.NotFound));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Format(Constants.EXCEPTION_MESSAGE, ex?.InnerException));
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
                    _logger.LogInformation(Constants.MODEL_STATE_NOT_VALID);
                    return BadRequest(new ApiResponse<object>(Constants.MODEL_STATE_NOT_VALID, (int)HttpStatusCode.BadRequest));
                }
                userDTO.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDTO.PasswordHash);
                var result = await _userServices.SaveAsync(userDTO);

                if (result != null)
                    return Ok(new ApiResponse<UserVM>(result, (int)HttpStatusCode.OK));
                else
                    return NotFound(new ApiResponse<UserVM>(Constants.DATA_NOT_FOUND_MESSAGE, (int)HttpStatusCode.NotFound));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{Constants.EXCEPTION_MESSAGE} - {ex?.InnerException}");
                return StatusCode(500, new ApiResponse<UserVM>(ex, (int)HttpStatusCode.InternalServerError));
            }
        }

        [HttpPut("update/{id:int}")]
        public async Task<ActionResult<UserVM>> UpdateAsync(int id, UserDTO userDTO)
        {
            try
            {
                if (!ModelState.IsValid || userDTO.Id <= 0 || id <= 0)
                {
                    _logger.LogInformation(Constants.MODEL_STATE_NOT_VALID);
                    return BadRequest(new ApiResponse<object>(Constants.MODEL_STATE_NOT_VALID, (int)HttpStatusCode.BadRequest));
                }

                var userData = await _userServices.GetByIdAsync(id);
                if (userData == null)
                    return NotFound(new ApiResponse<UserVM>(Constants.DATA_NOT_FOUND_MESSAGE, (int)HttpStatusCode.NotFound));

                userDTO.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDTO.PasswordHash);
                var result = await _userServices.UpdateAsync(userDTO);
                if (result != null)
                    return Ok(new ApiResponse<UserVM>(result, (int)HttpStatusCode.OK));
                else
                    return NotFound(new ApiResponse<UserVM>(Constants.DATA_NOT_FOUND_MESSAGE, (int)HttpStatusCode.NotFound));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Format(Constants.EXCEPTION_MESSAGE, ex?.InnerException));
                return StatusCode(500, new ApiResponse<UserVM>(ex, (int)HttpStatusCode.InternalServerError));
            }
        }

        [HttpDelete("delete/{id:int}")]
        public async Task<ActionResult<UserVM>> DeleteAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogInformation(Constants.INVALID_USERID_MESSAGE);
                    return BadRequest(new ApiResponse<UserVM>(Constants.INVALID_USERID_MESSAGE, (int)HttpStatusCode.BadRequest));
                }

                var result = await _userServices.DeleteAsync(id);
                if (result)
                    return Ok(new ApiResponse<string>(Constants.DELETE_MESSAGE, (int)HttpStatusCode.OK));
                else
                    return NotFound(new ApiResponse<UserVM>(Constants.DATA_NOT_FOUND_MESSAGE, (int)HttpStatusCode.NotFound));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Format(Constants.EXCEPTION_MESSAGE, ex?.InnerException));
                return StatusCode(500, new ApiResponse<UserVM>(ex, (int)HttpStatusCode.InternalServerError));
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserVM>> LoginAsync(LoginDTO loginDTO)
        {
            try
            {
                // Retrieve user by email
                var user = await _userServices.LoginAsync(loginDTO);

                if (user == null)
                {
                    // If no user found with the provided email, return 401 Unauthorized
                    return Unauthorized(new ApiResponse<object>(Constants.UNAUTHORIZED_MESSAGE, (int)HttpStatusCode.Unauthorized));
                }

                setTokenCookie(user?.RefreshTokens.FirstOrDefault()?.Token);

                // Password is valid, return user data or JWT token
                // For simplicity, assuming the user object is mapped to UserVM
                return Ok(new ApiResponse<UserVM>(user, (int)HttpStatusCode.OK));
            }
            catch (Exception ex)
            {
                // Log and return 500 Internal Server Error if an exception occurs
                _logger.LogError(ex, "An error occurred during login.");
                return StatusCode(500, new ApiResponse<UserVM>(ex, (int)HttpStatusCode.InternalServerError));
            }
        }

        [Authorize]
        [HttpGet("profile")]
        public async Task<ActionResult<UserVM>> GetUserProfileAsync()
        {
            var email = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            var userVM = await _userServices.GetUserByEmailAsync(email);

            if (userVM != null)
            {
                return Ok(new ApiResponse<UserVM>(userVM, (int)HttpStatusCode.OK));
            }
            else
            {
                return NotFound(new ApiResponse<UserVM>(Constants.DATA_NOT_FOUND_MESSAGE, (int)HttpStatusCode.NotFound));
            }
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<string>> RefreshTokenAsync()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            if (refreshToken == null)
                return Unauthorized(new ApiResponse<object>(Constants.INVALID_TOKEN_MESSAGE, (int)HttpStatusCode.Unauthorized));

            // Validate the refresh token
            var refreshTokenData = await _userServices.RefreshTokenAsync(refreshToken);

            if (refreshTokenData != null)
            {
                setTokenCookie(refreshTokenData?.Token);

                // Return the new access token
                return Ok(new ApiResponse<object>(new { token = refreshTokenData.User.JwtToken }, (int)HttpStatusCode.OK));
            }
            else
            {
                return Unauthorized(new ApiResponse<object>(Constants.INVALID_TOKEN_MESSAGE, (int)HttpStatusCode.Unauthorized));
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private void setTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.Now.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private string ipAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }

    }
}

