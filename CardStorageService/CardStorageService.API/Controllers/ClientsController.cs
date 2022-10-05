using AutoMapper;
using CardStorageService.API.Models.Requests;
using CardStorageService.API.Models.Responses;
using CardStorageService.Core.Interfaces;
using CardStorageService.Storage.Models;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CardStorageService.API.Controllers
{
    [Authorize]
    [Route("clients")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly ILogger<ClientsController> _logger;
        private readonly IClientService _service;
        private readonly IMapper _mapper;

        private readonly IValidator<ClientCreateRequest> _clientCreateRequestValidator;
        private readonly IValidator<ClientDeleteRequest> _clientDeleteRequestValidator;
        private readonly IValidator<ClientGetByIdRequest> _clientGetByIdRequestValidator;
        private readonly IValidator<ClientUpdateRequest> _clientUpdateRequestValidator;

        public ClientsController(
            ILogger<ClientsController> logger,
            IClientService service,
            IMapper mapper,
            IValidator<ClientCreateRequest> clientCreateRequestValidator,
            IValidator<ClientDeleteRequest> clientDeleteRequestValidator,
            IValidator<ClientGetByIdRequest> clientGetByIdRequestValidator,
            IValidator<ClientUpdateRequest> clientUpdateRequestValidator)
        {
            _logger = logger;
            _service = service;
            _mapper = mapper;
            _clientCreateRequestValidator = clientCreateRequestValidator;
            _clientDeleteRequestValidator = clientDeleteRequestValidator;
            _clientGetByIdRequestValidator = clientGetByIdRequestValidator;
            _clientUpdateRequestValidator = clientUpdateRequestValidator;
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> Create([FromBody] ClientCreateRequest request, CancellationToken cts)
        {
            try
            {
                var validationResult = _clientCreateRequestValidator.Validate(request);
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.ToDictionary());
                }

                var clientId = await _service.Create(_mapper.Map<Client>(request), cts);

                return Ok(new ClientCreateResponse()
                {
                    ClientId = clientId
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return Ok(new ClientCreateResponse()
                {
                    ErrorCode = 1001,
                    ErrorMessage = e.Message
                });
            }
        }

        [HttpPost("delete")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete([FromBody] ClientDeleteRequest request, CancellationToken cts)
        {
            try
            {
                var validationResult = _clientDeleteRequestValidator.Validate(request);
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.ToDictionary());
                }

                var count = await _service.Delete(request.ClientId, cts);
                return Ok(new ClientDeleteResponse()
                {
                    Count = count
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return Ok(new ClientDeleteResponse()
                {
                    ErrorCode = 1002,
                    ErrorMessage = e.Message
                });
            }
        }

        [HttpGet("getall")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cts)
        {
            try
            {
                var clients = await _service.GetAll(cts);
                return Ok(new ClientGetAllResponse()
                {
                    Clients = clients
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return Ok(new ClientGetAllResponse()
                {
                    ErrorCode = 1003,
                    ErrorMessage = e.Message
                });
            }
        }

        [HttpPost("getbyid")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById([FromBody] ClientGetByIdRequest request, CancellationToken cts)
        {
            try
            {
                var validationResult = _clientGetByIdRequestValidator.Validate(request);
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.ToDictionary());
                }

                var client = await _service.GetById(request.ClientId, cts);
                return Ok(new ClientGetByIdResponse()
                {
                    Client = client
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return Ok(new ClientGetByIdResponse()
                {
                    ErrorCode = 1004,
                    ErrorMessage = e.Message
                });
            }
        }

        [HttpPost("update")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update([FromBody] ClientUpdateRequest request, CancellationToken cts)
        {
            try
            {
                var validationResult = _clientUpdateRequestValidator.Validate(request);
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.ToDictionary());
                }

                var count = await _service.Update(_mapper.Map<Client>(request), cts);

                return Ok(new ClientUpdateResponse()
                {
                    Count = count
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return Ok(new ClientUpdateResponse()
                {
                    ErrorCode = 1005,
                    ErrorMessage = e.Message
                });
            }
        }
    }
}
