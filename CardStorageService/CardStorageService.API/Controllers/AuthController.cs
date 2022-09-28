using AutoMapper;
using CardStorageService.API.Models.Requests;
using CardStorageService.API.Models.Responses;
using CardStorageService.Core.Interfaces;
using CardStorageService.Core.Models;
using FluentValidation;
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

        private readonly IValidator<AuthLoginRequest> _authLoginRequestValidator;
        private readonly IValidator<AuthRegisterRequest> _authRegisterRequestValidator;

        public AuthController(
            ILogger<AuthController> logger,
            IAuthService service,
            IMapper mapper,
            IValidator<AuthLoginRequest> authLoginRequestValidator,
            IValidator<AuthRegisterRequest> authRegisterRequestValidator)
        {
            _logger = logger;
            _service = service;
            _mapper = mapper;
            _authLoginRequestValidator = authLoginRequestValidator;
            _authRegisterRequestValidator = authRegisterRequestValidator;
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> Register(AuthRegisterRequest request, CancellationToken cts)
        {
            try
            {
                var validationResult = _authRegisterRequestValidator.Validate(request);
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.ToDictionary());
                }

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
                var validationResult = _authLoginRequestValidator.Validate(request);
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.ToDictionary());
                }

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
