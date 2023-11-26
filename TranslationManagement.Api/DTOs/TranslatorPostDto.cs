
using System.ComponentModel.DataAnnotations;
using TranslationManagement.Api.Models;

namespace TranslationManagement.Api.DTOs
{
    public class TranslatorPostDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string HourlyRate { get; set; }
        [Required(ErrorMessage = "The Status field is required.")]
        public TranslatorStatus Status { get; set; }
        public string CreditCardNumber { get; set; }
    }
}