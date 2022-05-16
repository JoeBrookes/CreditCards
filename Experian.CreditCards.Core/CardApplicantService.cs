using Experian.CreditCards.Core.Interfaces;
using Experian.CreditCards.Models;
using Experian.CreditCards.Repo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Experian.CreditCards.Core
{
    public class CardApplicantService : ICardApplicantService
    {
        private readonly ICardDeciderEngine _cardDeciderEngine;
        private readonly ICardApplicantRepository _cardApplicantRepository;

        public CardApplicantService(ICardDeciderEngine cardDeciderEngine, ICardApplicantRepository cardApplicantRepository)
        {
            _cardDeciderEngine = cardDeciderEngine;
            _cardApplicantRepository = cardApplicantRepository;
        }

        public async Task<AssessedApplicant> Assess(Applicant applicant)
        {
            var inputCards = _cardApplicantRepository.GetCards();

            var availableCards = await _cardDeciderEngine.Run(applicant, inputCards);

            //Improvments - decisioning message from resource or similar
            var cardApplicant = new AssessedApplicant
            {
                AssessDate = DateTime.Now,
                DecisioningMessage = availableCards.Count == 0 ? "No credit cards available" : "Credit cards available",
                Applicant = applicant,
                AvailableCards = availableCards
            };

            await _cardApplicantRepository.AddAssessedApplicantAsync(cardApplicant);

            return cardApplicant;

        }
    }
}
