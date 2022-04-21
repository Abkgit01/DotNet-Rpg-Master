using AutoMapper;
using DotNet_Rpg_Master.DTOs;
using DotNet_Rpg_Master.Models;

namespace DotNet_Rpg_Master.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterDTO>();
            CreateMap<AddCharacterDTO, Character>();
        }
    }
}
