using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Experian.CreditCards.Models
{
    public class AssessedApplicant
    {
        [JsonIgnore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime AssessDate { get; set; }
        public string DecisioningMessage { get; set; }
        public Applicant Applicant { get; set; }
        public ICollection<Card> AvailableCards { get; set; }
    }
}