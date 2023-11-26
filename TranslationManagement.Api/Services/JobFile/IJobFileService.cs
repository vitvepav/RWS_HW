
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TranslationManagement.Api.Services.JobFileService
{
    public interface IJobFileService
    {
        Task<(string content, string? customerName)> ReadFileContent(IFormFile file);
    }
}