
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TranslationManagement.Api.DTOs;
using TranslationManagement.Api.Models;

namespace TranslationManagement.Api.Services.TranslationJobService
{
    public interface ITranslationJobService
    {
        Task<List<TranslationJobDto>> GetJobs(int? translatorId);
        Task<TranslationJobDto> AddJob(IFormCollection newJob);
        Task<TranslationJobDto> UpdateJobStatus(int jobId, JobStatus newStatus);
        Task DeleteJob(int jobId);
    }
}