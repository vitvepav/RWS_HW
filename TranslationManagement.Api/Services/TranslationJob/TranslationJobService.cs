
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TranslationManagement.Api.DTOs;
using TranslationManagement.Api.Models;
using TranslationManagement.Api.Services.JobFileService;
using TranslationManagement.Api.Services.TranslationJobService;

namespace TranslationManagement.Api.Services.TranslatorService
{
    public class TranslationJobService : ITranslationJobService
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        private readonly IJobFileService _jobFileService;
        private const double PRICE_PER_CHAR = 0.01;
        public TranslationJobService(IJobFileService jobFileService, IMapper mapper, AppDbContext context)
        {
            _jobFileService = jobFileService;
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<TranslationJobDto>> GetJobs(int? translatorId)
        {
            IQueryable<TranslationJob> query = _context.TranslationJobs;
            if (translatorId != null)
            {
                query = query.Where(curTranslationJob => curTranslationJob.TranslatorId == translatorId);
            }

            var res = await query.ToListAsync();
            return res.Select(item => _mapper.Map<TranslationJobDto>(item)).ToList();
        }

        public async Task<TranslationJobDto> AddJob(IFormCollection newJob)
        {
            TranslationJob jobToAdd = new TranslationJob
            {
                Status = JobStatus.New,
                Price = 0,
            };

            if (newJob.Files.Count > 0)
            {
                (string content, string? customerName) fileResult = await _jobFileService.ReadFileContent(newJob.Files[0]);
                jobToAdd.OriginalContent = fileResult.content;
                jobToAdd.CustomerName = fileResult.customerName;
            }
            else if (!string.IsNullOrEmpty(newJob["originalContent"]))
            {
                jobToAdd.OriginalContent = newJob["originalContent"];
            }
            else
            {
                throw new Exception("No content to translate");
            }

            Console.WriteLine($"Writing new job:");


            var added = await _context.TranslationJobs.AddAsync(jobToAdd);
            await _context.SaveChangesAsync();
            // also send notification
            return _mapper.Map<TranslationJobDto>(added.Entity);
        }

        public async Task<TranslationJobDto> UpdateJobStatus(int jobId, JobStatus newStatus)
        {
            var found = await _context.TranslationJobs.FirstOrDefaultAsync(curTranslator => curTranslator.Id == jobId);

            if (found == null)
            {
                throw new Exception($"Translation job {jobId} not found in database");
            }

            if (!CanUpdateJobStatus(found.Status, newStatus))
            {
                throw new Exception($"Cannot update job {jobId} with status {found.Status} to {newStatus}");
            }

            found.Status = newStatus;
            _context.Update(found);
            await _context.SaveChangesAsync();
            return _mapper.Map<TranslationJobDto>(found);
        }

        private bool CanUpdateJobStatus(JobStatus currentStatus, JobStatus newStatus)
        {
            return (currentStatus == JobStatus.New && newStatus == JobStatus.Completed) ||
                currentStatus == JobStatus.Completed || newStatus == JobStatus.New;
        }

        public async Task DeleteJob(int jobId)
        {
            _context.TranslationJobs.Remove(new TranslationJob() { Id = jobId });
            await _context.SaveChangesAsync();
        }
    }
}