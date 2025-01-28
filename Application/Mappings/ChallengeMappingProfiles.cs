using Application.Dtos.AnswerDtos;
using Application.Dtos.ChallengeDtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class ChallengeMappingProfiles : Profile
    {
        public ChallengeMappingProfiles()
        {
            CreateMap<Challenge, GetChallengeDto>();
            CreateMap<AddChallengeDto, GetChallengeDto>();
            CreateMap<AddChallengeDto, Challenge>();
            CreateMap<UpdateChallengeDto, Challenge>();
            CreateMap<Challenge, GetChallengeWithQuestionsDto>();
            CreateMap<Question, List<GetSimpleAnswerDto>>();
        }
    }
}
