using Application.Dtos.AnswerDtos;
using Application.Dtos.UserDtos;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappings
{
    public class AnswerMappingProfiles : Profile
    {
        public AnswerMappingProfiles()
        {
            CreateMap<Answer, GetAnswerDto>();
            CreateMap<AddAnswerDto, GetAnswerDto>();
            CreateMap<AddAnswerDto, Answer>();
        }
    }
}
