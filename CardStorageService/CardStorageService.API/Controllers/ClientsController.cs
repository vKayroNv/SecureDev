using CardStorageService.API.Models.Requests;
using CardStorageService.API.Models.Responses;
using CardStorageService.Core.Interfaces;
using CardStorageService.Storage.Models;
using Microsoft.AspNetCore.Mvc;

namespace CardStorageService.API.Controllers
{
    [Route("clients")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly ILogger<ClientsController> _logger;
        private readonly IClientService _service;

        public ClientsController(ILogger<ClientsController> logger, IClientService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> Create([FromBody] ClientCreateRequest request, CancellationToken cts)
        {
            try
            {
                var clientId = await _service.Create(new()
                {
                    Surname = request.Surname,
                    FirstName = request.FirstName,
                    Patronymic = request.Patronymic
                }, cts);
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
                var count = await _service.Update(new Client()
                {
                    ClientId = request.ClientId,
                    Surname = request.Surname,
                    FirstName = request.FirstName,
                    Patronymic = request.Patronymic
                }, cts);

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
