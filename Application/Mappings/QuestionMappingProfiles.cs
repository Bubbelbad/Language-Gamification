using Application.Dtos.QuestionDtos;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using System.Security.AccessControl;

namespace Application.Mappings
{
    public class QuestionMappingProfiles : Profile
    {
        public QuestionMappingProfiles()
        {
            CreateMap<Question, GetQuestionDto>();
            CreateMap<Question, List<GetQuestionDto>>();
        }
    }
}
