using AutoMapper;
using DotNet_Rpg_Master.Data;
using DotNet_Rpg_Master.DTOs;
using DotNet_Rpg_Master.DTOs.CharacterDTO;
using DotNet_Rpg_Master.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNet_Rpg_Master.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _dataContext;

        public CharacterService(IMapper mapper, DataContext dataContext)
        {
            this._mapper = mapper;
            this._dataContext = dataContext;
        }

        public async Task<ServiceResponse<List<GetCharacterDTO>>> AddCharacter(AddCharacterDTO newCharacter)
        {
            ServiceResponse<List<GetCharacterDTO>> serviceResponse = new ServiceResponse<List<GetCharacterDTO>>();
            Character character = _mapper.Map<Character>(newCharacter);
            await _dataContext.Characters.AddAsync(character); 
            await _dataContext.SaveChangesAsync();
            serviceResponse.Data = _dataContext.Characters.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDTO>>> GetAllCharacters(int userId)
        {
            ServiceResponse<List<GetCharacterDTO>> serviceResponse = new ServiceResponse<List<GetCharacterDTO>>();
            serviceResponse.Data = await _dataContext.Characters.Where(c => c.User.Id == userId).Select(c => _mapper.Map<GetCharacterDTO>(c)).ToListAsync();
            return serviceResponse;
        }
    

        public async Task<ServiceResponse<GetCharacterDTO>> GetCharacter(int Id)
        {
            ServiceResponse<GetCharacterDTO> serviceResponse = new ServiceResponse<GetCharacterDTO>();

            serviceResponse.Data = _mapper.Map<GetCharacterDTO>(await _dataContext.Characters.FirstOrDefaultAsync(x => x.Id == Id));
            return serviceResponse;
            
        }

        public async Task<ServiceResponse<GetCharacterDTO>> UpdateCharacter(UpdateCharacterDTO updateCharacter)
        {
            ServiceResponse<GetCharacterDTO> serviceResponse = new ServiceResponse<GetCharacterDTO>();
            try
            {
                Character character = await _dataContext.Characters.FirstOrDefaultAsync(c => c.Id == updateCharacter.Id);
                character.Name = updateCharacter.Name;
                character.HitPoints = updateCharacter.HitPoints;
                character.Strength = updateCharacter.Strength;
                character.Defense = updateCharacter.Defense;
                character.Intelligence = updateCharacter.Intelligence;
                character.Class = updateCharacter.Class;

                _dataContext.Characters.Update(character);
                await _dataContext.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetCharacterDTO>(character);
                
            }
            catch (Exception ex) 
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }  
            return serviceResponse;

        }

        public async Task<ServiceResponse<List<GetCharacterDTO>>> DeleteCharacter(int Id)
        {
            ServiceResponse<List<GetCharacterDTO>> serviceResponse = new ServiceResponse<List<GetCharacterDTO>>();
            Character character = await _dataContext.Characters.FirstOrDefaultAsync(c => c.Id == Id);
            if (character == null) 
            {
                serviceResponse.Data = new List<GetCharacterDTO>();
                serviceResponse.Success = false;
                serviceResponse.Message = "Character not found";
            }
            _dataContext.Characters.Remove(character);
            await _dataContext.SaveChangesAsync();
            return serviceResponse;
        }
    }
}

