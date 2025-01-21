using Application.Dtos.AnswerDtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class AnswerMappingProfiles : Profile
    {
        public AnswerMappingProfiles()
        {
            CreateMap<Answer, GetAnswerDto>();
            CreateMap<AddAnswerDto, GetAnswerDto>();
            CreateMap<AddAnswerDto, Answer>();
            CreateMap<Answer, UpdateAnswerDto>();
            CreateMap<Answer, GetSimpleAnswerDto>();
        }
    }
}
