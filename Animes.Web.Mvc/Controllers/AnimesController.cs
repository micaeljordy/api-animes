using Animes.Application.DTOs.Requests;
using Animes.Application.DTOs.Responses;
using Animes.Application.Interfaces;
using Animes.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Animes.Web.Mvc.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AnimesController : ControllerBase
    {
        private readonly IAnimeService _animeService;

        public AnimesController(IAnimeService animeService)
        {
            _animeService = animeService;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<AnimeResponseDTO>> GetAnime(int id)
        {
            var result = await _animeService.GetAnime(id);
            if(result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpGet]
        public async Task<ActionResult<AnimeViewModel>> GetAnimes(
            [FromQuery] int? index, 
            [FromQuery] int? take,
            [FromQuery] string? nome,
            [FromQuery] string? resumo,
            [FromQuery] string? diretor
            )
        {
            var filterAnimeRequest = new FilterAnimeRequest
            {
                Nome = nome,
                Resumo = resumo,
                Diretor = diretor
            };
            return Ok(await _animeService.GetAnimes(index, take, filterAnimeRequest));
        }
        [HttpPost]
        public async Task<ActionResult<AnimeResponseDTO>> CreateAnime([FromBody]CreateAnimeRequest createAnimeRequest)
        {
            var result = await _animeService.CreateAnime(createAnimeRequest);

            return CreatedAtAction(nameof(GetAnime), new {Id = result.Id}, result);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAnime(int id, [FromBody]UpdateAnimeRequest updateAnimeRequest)
        {
            var success = await _animeService.UpdateAnime(id, updateAnimeRequest);
            if(!success)
            {
                return NotFound();
            }

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAnime(int id)
        {
            var success = await _animeService.DeleteAnime(id);
            if(!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}