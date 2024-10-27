using System.ComponentModel.DataAnnotations;
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
    public class AnimeController : ControllerBase
    {
        private readonly IAnimeService _animeService;
        private readonly ILogger<AnimeController> _logger;
        public AnimeController(IAnimeService animeService, ILogger<AnimeController> logger)
        {
            _logger = logger;
            _animeService = animeService;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<AnimeResponseDTO>> GetAnime(int id)
        {
            _logger.LogInformation("Buscando o registro de {Entity} com o ID: {Id}.", "Anime", id);
            var result = await _animeService.GetAnime(id);
            if (result == null)
            {
                _logger.LogWarning("Nenhum registro de {Entity} encontrado com o ID: {Id}.", "Anime", id);
                return NotFound();
            }
            _logger.LogInformation("Registro de {Entity} encontrado: {Data}.", "Anime", result);
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
            var validationContext = new ValidationContext(filterAnimeRequest);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(filterAnimeRequest, validationContext, validationResults, true);

            if (!isValid)
            {
                return BadRequest(string.Join(Environment.NewLine, validationResults));
            }

            _logger.LogInformation("Buscando os registros de {Entity} com os dados: {Data}.", "Anime", filterAnimeRequest);
            var result = await _animeService.GetAnimes(index, take, filterAnimeRequest);
            _logger.LogInformation("Registro de {Entity} encontrado: {Data}.", "Anime", result);
            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult<AnimeResponseDTO>> CreateAnime([FromBody] CreateAnimeRequest createAnimeRequest)
        {
            _logger.LogInformation("Iniciando a criação do novo registro de {Entity} com os dados: {Data}.", "Anime", createAnimeRequest);
            var result = await _animeService.CreateAnime(createAnimeRequest);
            _logger.LogInformation("Registro de {Entity} criado com sucesso. ID: {Id}.", "Anime", result.Id);
            return CreatedAtAction(nameof(GetAnime), new { Id = result.Id }, result);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAnime(int id, [FromBody] UpdateAnimeRequest updateAnimeRequest)
        {
            _logger.LogInformation("Iniciando a atualização do registro de {Entity} com o ID: {Id}. Dados novos: {NewData}.", "Anime", id, updateAnimeRequest);
            var success = await _animeService.UpdateAnime(id, updateAnimeRequest);
            if (!success)
            {
                _logger.LogWarning("Nenhum registro de {Entity} encontrado com o ID: {Id}.", "Anime", id);
                return NotFound();
            }
            _logger.LogInformation("Registro de {Entity} encontrado: {Data}.", "Anime", updateAnimeRequest);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAnime(int id)
        {
            _logger.LogInformation("Iniciando a exclusão do registro de {Entity} com o ID: {Id}.", "Anime", id);
            var success = await _animeService.DeleteAnime(id);
            if (!success)
            {
                _logger.LogWarning("Falha ao encontrar o registro de {Entity} com o ID: {Id} para exclusão.", "Anime", id);
                return NotFound();
            }
            _logger.LogInformation("Registro de {Entity} com o ID: {Id} excluído com sucesso.", "Anime", id);
            return NoContent();
        }
    }
}