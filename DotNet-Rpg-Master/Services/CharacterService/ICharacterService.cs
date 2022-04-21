using DotNet_Rpg_Master.DTOs;
using DotNet_Rpg_Master.DTOs.CharacterDTO;
using DotNet_Rpg_Master.Models;

namespace DotNet_Rpg_Master.Services.CharacterService
{
    public interface ICharacterService
    {
        Task<ServiceResponse<GetCharacterDTO>> GetCharacter(int Id);
        Task<ServiceResponse<List<GetCharacterDTO>>> GetAllCharacters(int userId);
        Task<ServiceResponse<List<GetCharacterDTO>>> AddCharacter(AddCharacterDTO newCharacter);
        Task<ServiceResponse<GetCharacterDTO>> UpdateCharacter(UpdateCharacterDTO updateCharacter);
        Task<ServiceResponse<List<GetCharacterDTO>>> DeleteCharacter(int Id);
    }
}
