using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TranslationManagement.Api.Controlers;
using TranslationManagement.Api.DTOs;
using TranslationManagement.Api.Models;
using TranslationManagement.Api.Services.TranslationJobService;

namespace TranslationManagement.Api.Controllers
{
    [ApiController]
    [Route("api/jobs")]
    public class TranslationJobController : ControllerBase
    {
        private readonly ILogger<TranslatorManagementController> _logger;
        private readonly ITranslationJobService _translationJobService;

        public TranslationJobController(ITranslationJobService translationJobService, ILogger<TranslatorManagementController> logger)
        {
            _translationJobService = translationJobService;
            _logger = logger;
        }

        [HttpGet("")]
        public async Task<ActionResult<List<TranslationJob>>> GetJobs(int? translatorId)
        {
            return Ok(await _translationJobService.GetJobs(translatorId));
        }

        [HttpPost("")]
        public async Task<ActionResult<TranslationJob>> CreateJob()
        {
            // return Ok(new TranslationJob());
            return Ok(await _translationJobService.AddJob(Request.Form));
        }

        // [HttpPost]
        // public bool CreateJobWithFile(IFormFile file, string customer)
        // {
        //     var reader = new StreamReader(file.OpenReadStream());
        //     string content;

        //     if (file.FileName.EndsWith(".txt"))
        //     {
        //         content = reader.ReadToEnd();
        //     }
        //     else if (file.FileName.EndsWith(".xml"))
        //     {
        //         var xdoc = XDocument.Parse(reader.ReadToEnd());
        //         content = xdoc.Root.Element("Content").Value;
        //         customer = xdoc.Root.Element("Customer").Value.Trim();
        //     }
        //     else
        //     {
        //         throw new NotSupportedException("unsupported file");
        //     }

        //     var newJob = new TranslationJob()
        //     {
        //         OriginalContent = content,
        //         TranslatedContent = "",
        //         CustomerName = customer,
        //     };

        //     SetPrice(newJob);

        //     return CreateJob(newJob);
        // }

        [HttpPut("{id}")]
        public async Task<ActionResult<TranslationJobDto>> UpdateJobStatus(int id, int translatorId, JobStatus newStatus)
        {
            _logger.LogInformation("Job status update request received: " + newStatus + " for job " + id.ToString() + " by translator " + translatorId);
            return Ok(await _translationJobService.UpdateJobStatus(id, newStatus));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob(int id)
        {
            await _translationJobService.DeleteJob(id);
            return Ok();
        }
    }
}