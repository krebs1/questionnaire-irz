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
            
           // CreateMap<DeleteQuestionnaireDTO, Questionnaire>();
        }
    }
}