using TranslationManagement.Api.Models;

namespace TranslationManagement.Api.DTOs
{
    public class TranslationJobDto
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public JobStatus Status { get; set; }
        public string OriginalContent { get; set; }
        public string TranslatedContent { get; set; }
        public int? TranslatorId { get; set; }
        public double Price { get; set; }
    }
}