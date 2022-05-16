using Experian.CreditCards.Models;

namespace Experian.CreditCards.Core.Interfaces
{
    public interface ICardDeciderEngine
    {
        public Task<List<Card>> Run(Applicant applicant, List<Card> inputCards);
    }
}
