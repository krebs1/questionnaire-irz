using AutoMapper;
using Entities.Models;
using System.Linq;
using questionnaire.DTO;

namespace questionnaire
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            
            CreateMap<CreateQuestionnaireDTO, Questionnaire>();

            CreateMap<UpdateQuestionnaireDTO, Questionnaire>();
            
            CreateMap<CreateQuestionDTO, Question>();
            
            CreateMap<UpdateQuestionDTO, Question>();
            
            CreateMap<CreateVariantDTO, Variant>();
            
            CreateMap<UpdateVariantDTO, Variant>();
            
        }
    }
}