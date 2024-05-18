using Microsoft.AspNetCore.Mvc;
using MiPokemonFavorito.Business.Services;
using MiPokemonFavorito.Business.Services.Pok;

namespace MiPokemonFavorito.Controllers;

[ApiController]
[Route("[controller]")]
public class PokemonController : ControllerBase
{
    private readonly PokemonService _pokemonService;

    public PokemonController(PokemonService pokemonService)
    {
        _pokemonService = pokemonService;
    }

    [HttpGet("{pokemonName}")]
    public async Task<ActionResult<Pokemon>> Get(string pokemonName)
    {
        var pokemon = await _pokemonService.GetFavoritePokemon(pokemonName);
        return Ok(pokemon);
    }
}
