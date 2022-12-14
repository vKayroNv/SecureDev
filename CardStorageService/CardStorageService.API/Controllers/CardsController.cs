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
    [Route("cards")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly ILogger<CardsController> _logger;
        private readonly ICardService _service;
        private readonly IMapper _mapper;

        private readonly IValidator<CardCreateRequest> _cardCreateRequestValidator;
        private readonly IValidator<CardDeleteRequest> _cardDeleteRequestValidator;
        private readonly IValidator<CardGetByClientIdRequest> _cardGetByClientIdRequestValidator;
        private readonly IValidator<CardGetByIdRequest> _cardGetByIdRequestValidator;
        private readonly IValidator<CardUpdateRequest> _cardUpdateRequestValidator;

        public CardsController(
            ILogger<CardsController> logger,
            ICardService service, IMapper mapper,
            IValidator<CardCreateRequest> cardCreateRequestValidator,
            IValidator<CardDeleteRequest> cardDeleteRequestValidator,
            IValidator<CardGetByClientIdRequest> cardGetByClientIdRequestValidator,
            IValidator<CardGetByIdRequest> cardGetByIdRequestValidator,
            IValidator<CardUpdateRequest> cardUpdateRequestValidator)
        {
            _logger = logger;
            _service = service;
            _mapper = mapper;
            _cardCreateRequestValidator = cardCreateRequestValidator;
            _cardDeleteRequestValidator = cardDeleteRequestValidator;
            _cardGetByClientIdRequestValidator = cardGetByClientIdRequestValidator;
            _cardGetByIdRequestValidator = cardGetByIdRequestValidator;
            _cardUpdateRequestValidator = cardUpdateRequestValidator;
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> Create([FromBody] CardCreateRequest request, CancellationToken cts)
        {
            try
            {
                var validationResult = _cardCreateRequestValidator.Validate(request);
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.ToDictionary());
                }

                var cardId = await _service.Create(_mapper.Map<Card>(request), cts);

                return Ok(new CardCreateResponse()
                {
                    CardId = cardId.ToString()
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return Ok(new CardCreateResponse()
                {
                    ErrorCode = 1001,
                    ErrorMessage = e.Message
                });
            }
        }

        [HttpPost("delete")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete([FromBody] CardDeleteRequest request, CancellationToken cts)
        {
            try
            {
                var validationResult = _cardDeleteRequestValidator.Validate(request);
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.ToDictionary());
                }

                var count = await _service.Delete(request.CardId, cts);
                return Ok(new CardDeleteResponse()
                {
                    Count = count
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return Ok(new CardDeleteResponse()
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
                var cards = await _service.GetAll(cts);
                return Ok(new CardGetAllResponse()
                {
                    Cards = cards
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return Ok(new CardGetAllResponse()
                {
                    ErrorCode = 1003,
                    ErrorMessage = e.Message
                });
            }
        }

        [HttpPost("getbyclientid")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByClientId([FromBody] CardGetByClientIdRequest request, CancellationToken cts)
        {
            try
            {
                var validationResult = _cardGetByClientIdRequestValidator.Validate(request);
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.ToDictionary());
                }

                var cards = await _service.GetByClientId(request.ClientId, cts);
                return Ok(new CardGetByClientIdResponse()
                {
                    Cards = cards
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return Ok(new CardGetByClientIdResponse()
                {
                    ErrorCode = 1004,
                    ErrorMessage = e.Message
                });
            }
        }

        [HttpPost("getbyid")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById([FromBody] CardGetByIdRequest request, CancellationToken cts)
        {
            try
            {
                var validationResult = _cardGetByIdRequestValidator.Validate(request);
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.ToDictionary());
                }

                var card = await _service.GetById(request.CardId, cts);
                return Ok(new CardGetByIdResponse()
                {
                    Card = card
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return Ok(new CardGetByIdResponse()
                {
                    ErrorCode = 1005,
                    ErrorMessage = e.Message
                });
            }
        }

        [HttpPost("update")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update([FromBody] CardUpdateRequest request, CancellationToken cts)
        {
            try
            {
                var validationResult = _cardUpdateRequestValidator.Validate(request);
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.ToDictionary());
                }

                var count = await _service.Update(_mapper.Map<Card>(request), cts);

                return Ok(new CardUpdateResponse()
                {
                    Count = count
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return Ok(new CardUpdateResponse()
                {
                    ErrorCode = 1006,
                    ErrorMessage = e.Message
                });
            }
        }
    }
}
