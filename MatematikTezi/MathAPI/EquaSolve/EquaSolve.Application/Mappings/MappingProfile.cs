using AutoMapper;
using EquaSolve.Application.DTOs;
using EquaSolve.Domain.Entities;

namespace EquaSolve.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<MathResult, EquationResponseDto>()
                .ForMember(dest => dest.Results, opt => opt.MapFrom(src => src.VariableValues))
                .ForMember(dest => dest.Latex, opt => opt.MapFrom(src => src.LatexRepresentation));
        }
    }
}
