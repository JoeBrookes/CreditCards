using Experian.CreditCards.Core.Interfaces;
using Experian.CreditCards.Models;
using Experian.CreditCards.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RulesEngine.Models;
using static RulesEngine.Extensions.ListofRuleResultTreeExtension;

namespace Experian.CreditCards.Core
{
    public class CardDeciderEngine : ICardDeciderEngine
    {
        private RuleEngineOptions _options;

        public CardDeciderEngine(IOptions<RuleEngineOptions> options)
        {
            _options = options.Value;
        }

        public async Task<List<Card>> Run(Applicant applicant, List<Card> inputCards)
        {
            //todo Improvment - method could be broken down a bit more

            var workflow = GetWorkflow();
            var engine = new RulesEngine.RulesEngine(workflow.ToArray(), null);

            //Check applicant elligible
            var elligible = false;
            var applicantResults = await engine.ExecuteAllRulesAsync("Applicant", applicant);
            var availableCards = new List<Card>();

            applicantResults.OnSuccess((eventName) =>
            {
                elligible = true;
            });

            //Check cards available
            if (elligible)
            {
                foreach (var card in inputCards)
                {
                    var cardResults = await engine.ExecuteAllRulesAsync(card.RuleName, applicant);
                    cardResults.OnSuccess((eventname) =>
                    {
                        availableCards.Add(card);
                    });

                }
            }

            return availableCards;
        }

        private List<Workflow> GetWorkflow()
        {
            var files = Directory.GetFiles(Directory.GetCurrentDirectory(), _options.RulesetPath, SearchOption.AllDirectories);
            if (files == null || files.Length == 0)
            {
                throw new Exception("Rules not found");
            }
            var fileData = File.ReadAllText(files[0]);

            var workflow = JsonConvert.DeserializeObject<List<Workflow>>(fileData);
            if (workflow == null)
            {
                throw new Exception("Unable to deserialize rules");
            }

            return workflow;
        }
    }
}