using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Experian.CreditCards.Models
{
    public class Card
    {
        [JsonIgnore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Apr { get; set; }
        public string Promo { get; set; }
        [JsonIgnore]
        public string RuleName { get; set; }
        [JsonIgnore]
        public ICollection<AssessedApplicant> AssessedApplicants { get; set; }  
    }
}
