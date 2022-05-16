using Experian.CreditCards.Models;

namespace Experian.CreditCards.Repo.Interfaces
{
    public interface ICardApplicantRepository
    {
        public Task AddAssessedApplicantAsync(AssessedApplicant assessedApplicant);

        public List<Card> GetCards();
    }
}
