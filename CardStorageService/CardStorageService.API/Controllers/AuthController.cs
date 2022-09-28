using AutoMapper;
using CardStorageService.API.Models.Requests;
using CardStorageService.API.Models.Responses;
using CardStorageService.Core.Interfaces;
using CardStorageService.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CardStorageService.API.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IAuthService _service;
        private readonly IMapper _mapper;

        public AuthController(ILogger<AuthController> logger, IAuthService service, IMapper mapper)
        {
            _logger = logger;
            _service = service;
            _mapper = mapper;
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> Register(AuthRegisterRequest request, CancellationToken cts)
        {
            try
            {
                var token = await _service.Register(_mapper.Map<AccountDto>(request), cts);

                return Ok(new AuthRegisterResponse()
                {
                    Token = token
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return Ok(new AuthRegisterResponse()
                {
                    ErrorCode = 1001,
                    ErrorMessage = e.Message
                });
            }
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login(AuthLoginRequest request, CancellationToken cts)
        {
            try
            {
                var token = await _service.Login(request.EMail, request.Password, cts);
                return Ok(new AuthLoginResponse()
                {
                    Token = token
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return Ok(new AuthLoginResponse()
                {
                    ErrorCode = 1002,
                    ErrorMessage = e.Message
                });
            }
        }
    }
}
