using Application.Dtos.UserChallengeDtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class UserChallengeMappingProfiles : Profile
    {
        public UserChallengeMappingProfiles()
        {
            CreateMap<UserChallenge, GetUserChallengeDto>();
            CreateMap<AddUserChallengeDto, GetUserChallengeDto>();
            CreateMap<AddUserChallengeDto, UserChallenge>();
        }
    }
}
