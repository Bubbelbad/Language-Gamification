using Application.Dtos.QuestionDtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class QuestionMappingProfiles : Profile
    {
        public QuestionMappingProfiles()
        {
            CreateMap<Question, GetQuestionDto>();
        }
    }
}
