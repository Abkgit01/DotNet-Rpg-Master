using DotNet_Rpg_Master.DTOs;
using DotNet_Rpg_Master.DTOs.CharacterDTO;
using DotNet_Rpg_Master.Models;
using DotNet_Rpg_Master.Services.CharacterService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DotNet_Rpg_Master.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _iCharacterService;
        public CharacterController(ICharacterService characterService)
        {
            _iCharacterService = characterService;
        }

        [HttpGet("get-all-character")]
        public async Task<IActionResult> GetAllCharacters()
        {

            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            return Ok(await _iCharacterService.GetAllCharacters(userId));
        }

        [HttpGet("get-character")]
        public async Task<IActionResult> GetCharacters(int Id)
        {
            return Ok(await _iCharacterService.GetCharacter(Id));
        }

        [HttpPost]
        public async Task<IActionResult> AddCharacter(AddCharacterDTO newCharacter)
        {
            return Ok(await _iCharacterService.AddCharacter(newCharacter));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCharacter(UpdateCharacterDTO updateCharacter)
        {
            ServiceResponse<GetCharacterDTO> serviceResponse = await _iCharacterService.UpdateCharacter(updateCharacter);
            if (serviceResponse.Data == null)
            {
                return NotFound(serviceResponse);
            }
            return Ok(serviceResponse);
        }

        [HttpDelete("Id")]
        public async Task<IActionResult> DeleteCharacter(int Id)
        {
            ServiceResponse<List<GetCharacterDTO>> serviceResponse = await _iCharacterService.DeleteCharacter(Id);
            if (serviceResponse.Data == null)
            {
                return NotFound(serviceResponse);
            }
            return Ok(serviceResponse);
        }
    }
}
