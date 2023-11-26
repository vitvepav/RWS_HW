using TranslationManagement.Api.Models;

namespace TranslationManagement.Api.DTOs
{
    public class TranslationJobPostDto
    {
        public string CustomerName { get; set; }
        public string? OriginalContent { get; set; }
        public string? FileToTranslate { get; set; }
    }
}