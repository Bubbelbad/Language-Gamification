using Application.Dtos.AnswerDtos;
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
            CreateMap<Question, List<GetQuestionDto>>();
            CreateMap<Question, GetSimpleAnswerDto>();
            CreateMap<Question, GetQuestionWithAnswersDto>()
                .ForMember(dest => dest.Answers, opt => opt.MapFrom(src => src.Answers));
        }
    }
}
