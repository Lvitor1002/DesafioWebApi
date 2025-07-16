namespace GruposPokemonApi.Models
{
    public class Teams
    {
        public string? Owner{ get; set; }
        public List<Pokemon> Pokemons { get; set; } = new List<Pokemon>();
    }
}
