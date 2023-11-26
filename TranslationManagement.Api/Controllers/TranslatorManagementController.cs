using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TranslationManagement.Api.DTOs;
using TranslationManagement.Api.Models;
using TranslationManagement.Api.Services.TranslatorService;

namespace TranslationManagement.Api.Controlers
{
    [ApiController]
    [Route("api/translators")]
    public class TranslatorManagementController : ControllerBase
    {
        private readonly ILogger<TranslatorManagementController> _logger;
        private readonly ITranslatorService _traslatorService;

        public TranslatorManagementController(ITranslatorService translatorService, ILogger<TranslatorManagementController> logger)
        {
            _logger = logger;
            _traslatorService = translatorService;
        }

        [HttpGet("")]
        public async Task<ActionResult<List<Translator>>> GetTranslators(string name)
        {
            return Ok(await _traslatorService.GetTranslators(name));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TranslatorDto>> GetTranslator(int id)
        {
            return Ok(await _traslatorService.GetTranslator(id));
        }

        [HttpPost]
        public async Task<ActionResult<Translator>> AddTranslator(TranslatorPostDto translator)
        {
            return Ok(await _traslatorService.AddTranslator(translator));
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTranslatorStatus(int id, [FromBody] TranslatorPostDto translator)
        {
            return Ok(_traslatorService.UpdateTranslatorStatus(id, translator.Status));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTranslator(int id)
        {
            await _traslatorService.DeleteTranslator(id);
            return Ok();
        }
    }
}