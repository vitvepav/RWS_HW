using System.Text.Json.Serialization;

namespace TranslationManagement.Api.Models
{
    public class TranslationJob
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public JobStatus Status { get; set; }
        public string OriginalContent { get; set; }
        public string TranslatedContent { get; set; }
        public int? TranslatorId { get; set; }
        public double Price { get; set; }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum JobStatus
    {
        New,
        InProgress,
        Completed
    }
}