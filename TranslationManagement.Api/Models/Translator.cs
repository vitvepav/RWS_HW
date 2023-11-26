
using System.Text.Json.Serialization;

namespace TranslationManagement.Api.Models
{
    public class Translator
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string HourlyRate { get; set; }
        public TranslatorStatus Status { get; set; }
        public string CreditCardNumber { get; set; }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TranslatorStatus
    {
        Applicant,
        Certified,
        Deleted
    };
}