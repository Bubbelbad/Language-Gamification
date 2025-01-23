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
            CreateMap<Question, GetQuestionWithAnswersDto>()
                .ForMember(dest => dest.Answers, opt => opt.MapFrom(src => src.Answers));
        }
    }
}
