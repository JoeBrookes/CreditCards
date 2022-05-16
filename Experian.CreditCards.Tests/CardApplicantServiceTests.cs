using Experian.CreditCards.Core;
using Experian.CreditCards.Models;
using Experian.CreditCards.Options;
using Experian.CreditCards.Repo.Interfaces;
using Moq;


namespace Experian.CreditCards.Tests
{
    public class CardApplicantServiceTests
    {
        //Todo Improvement - more robust testing around age and salary boundaries
        [Fact]
        public void Assess_ApplicantUnder18_ReturnNoCardsAvailable()
        {
            var cardDeciderEngine = new CardDeciderEngine(Microsoft.Extensions.Options.Options.Create(new RuleEngineOptions { RulesetPath = "RuleSets\\CreditCards.json"}));
            var mockRepo = new Mock<ICardApplicantRepository>();
            var service = new CardApplicantService(cardDeciderEngine, mockRepo.Object);
            mockRepo.Setup(x => x.GetCards()).Returns(GetDummyCards());

            var applicant = new Applicant { FirstName = "young", LastName = "Gun", Dob = DateTime.Now.AddYears(-17), Income = 15000 };

            var result = service.Assess(applicant).Result;

            Assert.NotNull(result);
            Assert.Equal("No credit cards available", result.DecisioningMessage);
            Assert.Equal(0, result.AvailableCards.Count);

        }

        [Fact]
        public void Assess_ApplicantOver18AndIncome30000_ReturnVanquis()
        {
            var cardDeciderEngine = new CardDeciderEngine(Microsoft.Extensions.Options.Options.Create(new RuleEngineOptions { RulesetPath = "RuleSets\\CreditCards.json" }));
            var mockRepo = new Mock<ICardApplicantRepository>();
            var service = new CardApplicantService(cardDeciderEngine, mockRepo.Object);
            mockRepo.Setup(x => x.GetCards()).Returns(GetDummyCards());

            var applicant = new Applicant { FirstName = "Average", LastName = "Joe", Dob = DateTime.Now.AddYears(-19), Income = 30000 };

            var result = service.Assess(applicant).Result;

            Assert.NotNull(result);
            Assert.Equal("Credit cards available", result.DecisioningMessage);
            Assert.Equal(1, result.AvailableCards.Count);
            Assert.Equal("Vanquis", result.AvailableCards.First().Name);

        }

        [Fact]
        public void Assess_ApplicantOver18AndIncome31000_ReturnBarclaycard()
        {
            var cardDeciderEngine = new CardDeciderEngine(Microsoft.Extensions.Options.Options.Create(new RuleEngineOptions { RulesetPath = "RuleSets\\CreditCards.json" }));
            var mockRepo = new Mock<ICardApplicantRepository>();
            var service = new CardApplicantService(cardDeciderEngine, mockRepo.Object);
            mockRepo.Setup(x => x.GetCards()).Returns(GetDummyCards());

            var applicant = new Applicant { FirstName = "Mr", LastName = "Overtime", Dob = DateTime.Now.AddYears(-19), Income = 31000 };

            var result = service.Assess(applicant).Result;

            Assert.NotNull(result);
            Assert.Equal("Credit cards available", result.DecisioningMessage);
            Assert.Equal(1, result.AvailableCards.Count);
            Assert.Equal("Barclaycard", result.AvailableCards.First().Name);

        }


        private List<Card> GetDummyCards()
        {
            return new List<Card> { 
                new Card { Name = "Barclaycard", RuleName = "Barclaycard" }, 
                new Card { Name = "Vanquis", RuleName = "Vanquis" } };
        }
    }
}