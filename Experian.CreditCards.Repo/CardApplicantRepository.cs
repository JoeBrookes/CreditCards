using Experian.CreditCards.Models;
using Experian.CreditCards.Repo.Interfaces;

namespace Experian.CreditCards.Repo
{
    public class CardApplicantRepository : ICardApplicantRepository
    {
        //Todo Improvement - More consideration to a repository design in production code
        private readonly CreditCardContext _context;
        public CardApplicantRepository(CreditCardContext context)
        {
            _context = context;
        }
        public async Task AddAssessedApplicantAsync(AssessedApplicant assessedApplicant)
        {
            await _context.AssesssedApplicants.AddAsync(assessedApplicant);
            await _context.SaveChangesAsync();
        }

        public List<Card> GetCards()
        {
            return  _context.Cards.ToList();
        }
    }
}
