using Experian.CreditCards.Core.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Experian.CreditCards.Models
{
    public class Applicant
    {
        [JsonIgnore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        
        [MaxLength(100)]
        [Required]
        public string? FirstName { get; set; }

        [MaxLength(100)]
        [Required]
        public string? LastName { get; set; }

        [Required]
        public DateTime? Dob { get; set; }

        [Required]
        public decimal? Income { get; set; }

        [JsonIgnore]
        [NotMapped]
        public int Age { get { return Dob.GetValueOrDefault().Age(); } private set { } }
    }
}