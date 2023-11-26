
using TranslationManagement.Api.Models;

namespace TranslationManagement.Api.DTOs
{
    public class TranslatorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string HourlyRate { get; set; }
        public TranslatorStatus Status { get; set; }
    }
}