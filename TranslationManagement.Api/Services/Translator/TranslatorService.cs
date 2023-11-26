
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TranslationManagement.Api.DTOs;
using TranslationManagement.Api.Models;

namespace TranslationManagement.Api.Services.TranslatorService
{
    public class TranslatorService : ITranslatorService
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        public TranslatorService(IMapper mapper, AppDbContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<TranslatorDto>> GetTranslators(string name)
        {
            IQueryable<Translator> query = _context.Translators;
            if (name != null)
            {
                query = query.Where(curTranslator => curTranslator.Name == name);
            }

            var res = await query.ToListAsync();
            return res.Select(item => _mapper.Map<TranslatorDto>(item)).ToList();
        }

        public async Task<TranslatorDto> GetTranslator(int id)
        {
            Translator found = await _context.Translators.FirstOrDefaultAsync(curTranslator => curTranslator.Id == id);
            if (found == null)
            {
                throw new Exception($"Translator {id} not found in database");
            }

            return _mapper.Map<TranslatorDto>(found);
        }

        public async Task<TranslatorDto> AddTranslator(TranslatorPostDto newTranslator)
        {
            var added = await _context.Translators.AddAsync(_mapper.Map<Translator>(newTranslator));
            await _context.SaveChangesAsync();
            return _mapper.Map<TranslatorDto>(added.Entity);
        }

        public async Task<TranslatorDto> UpdateTranslatorStatus(int translatorId, TranslatorStatus newStatus)
        {
            var found = await _context.Translators.FirstOrDefaultAsync(curTranslator => curTranslator.Id == translatorId);

            if (found == null)
            {
                throw new Exception($"Translator {translatorId} not found in database");
            }

            found.Status = newStatus;
            _context.Update(found);
            await _context.SaveChangesAsync();
            return _mapper.Map<TranslatorDto>(found);
        }

        public async Task DeleteTranslator(int translatorId)
        {
            _context.Translators.Remove(new Translator() { Id = translatorId });
            await _context.SaveChangesAsync();
        }
    }
}