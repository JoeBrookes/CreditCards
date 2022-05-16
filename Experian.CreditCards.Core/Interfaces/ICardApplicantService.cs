using Experian.CreditCards.Models;

namespace Experian.CreditCards.Core.Interfaces
{
    public interface ICardApplicantService
    {
        public Task<AssessedApplicant> Assess(Applicant applicant);
    }
}