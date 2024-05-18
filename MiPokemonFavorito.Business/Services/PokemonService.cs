using MiPokemonFavorito.Business.Services.Pokemones;
using System.Net.Http.Json;
using System.Text.Json;

namespace MiPokemonFavorito.Business.Services;

public class PokemonService
{
    private readonly HttpClient _client;

    public PokemonService(HttpClient client)
    {
        _client = client;
    }

    public async Task<Pokemon> GetFavoritePokemon(string pokeName)
    {
        var response = await _client.GetAsync($"https://pokeapi.co/api/v2/pokemon/{pokeName}");
        response.EnsureSuccessStatusCode();

        var pokemonData = await response.Content.ReadFromJsonAsync<JsonElement>();

        var pokemon = new Pokemon
        {
            Nombre = pokeName,
            Tipo = pokemonData.GetProperty("types").EnumerateArray().First().GetProperty("type").GetProperty("name").GetString(),
            Url_Del_Sprite = pokemonData.GetProperty("sprites").GetProperty("front_default").GetString(),
            Lista_De_Movimientos = pokemonData.GetProperty("moves").EnumerateArray().Select(m => m.GetProperty("move").GetProperty("name").GetString()).ToList()
        };


        return pokemon;
    }
    
}
