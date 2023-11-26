


using TranslationManagement.Api.DTOs;
using TranslationManagement.Api.Models;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<TranslatorPostDto, Translator>();
        CreateMap<Translator, TranslatorDto>();
        CreateMap<TranslatorPostDto, TranslatorDto>();
        CreateMap<TranslationJob, TranslationJobDto>();
        CreateMap<TranslationJobDto, TranslationJob>();
    }
}