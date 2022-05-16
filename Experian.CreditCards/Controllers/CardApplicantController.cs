using Experian.CreditCards.Core.Interfaces;
using Experian.CreditCards.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Experian.CreditCards.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardApplicantController : ControllerBase
    {
        private readonly ILogger<CardApplicantController> _logger;
        private readonly ICardApplicantService _cardApplicantService;

        public CardApplicantController(ILogger<CardApplicantController> logger, ICardApplicantService cardApplicantService)
        {
            _logger = logger;
            _cardApplicantService = cardApplicantService;
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AssessedApplicant))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostAsync(Applicant applicant)
        {
            //Todo Improvements -
            //  with a bit more understanding of the domain this could perhaps be more "restful"
            //  use dtos/automapper and map to domain models
            //  Add a validator method to api input types if more complex validation required
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var assesedApplicant = await _cardApplicantService.Assess(applicant);

                return Ok(assesedApplicant);
            }
            catch(Exception ex)
            {
                _logger.LogError("CardApplicant POST - Failed", ex);
                return BadRequest();
            }
        }
    }
}
