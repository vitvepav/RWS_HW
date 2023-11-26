
using System.Collections.Generic;
using System.Threading.Tasks;
using TranslationManagement.Api.DTOs;
using TranslationManagement.Api.Models;

namespace TranslationManagement.Api.Services.TranslatorService
{
    public interface ITranslatorService
    {
        Task<List<TranslatorDto>> GetTranslators(string name);
        Task<TranslatorDto> GetTranslator(int id);
        Task<TranslatorDto> AddTranslator(TranslatorPostDto newTranslator);
        Task<TranslatorDto> UpdateTranslatorStatus(int translatorId, TranslatorStatus newStatus);
        Task DeleteTranslator(int translatorId);
    }
}