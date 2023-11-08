using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pokeAPI.Models;

namespace pokeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokeAPIController : ControllerBase
    {
        List<Pokemon_Entries> pokeDex = PokemonAPIDAL.SearchPokedex().pokemon_entries.ToList();
        [HttpGet]
        public pokemon getPokeDex()
        {
            return PokemonAPIDAL.SearchPokedex();
        }
        [HttpGet("Random")]
        public Pokemon_Species getRandomPokemon() 
        {
            
            Random rnd = new Random();
            int num = rnd.Next(0,pokeDex.Count);
            return pokeDex[num].pokemon_species;
        }
        [HttpGet("RandomTeam")]
        public List<Pokemon_Species> getRandomTeam()
        {
            List<Pokemon_Species> rndTeam = new List<Pokemon_Species>();
            for(int i = 0; i<6; i++)
            {
                rndTeam.Add(getRandomPokemon());
            }
            return rndTeam;
        }
        [HttpGet("Name")]
        public List<Pokemon_Species> getByName(string name)
        {
            List<Pokemon_Species> species = new List<Pokemon_Species>();
            pokeDex.Where(e => e.pokemon_species.name.Contains(name)).ToList()
                .ForEach(s => species.Add(s.pokemon_species));
            
            /*foreach(Pokemon_Entries entry in sortedList)
            {
                species.Add(entry.pokemon_species);
            }*/
            return species;

            //return pokeDex.FirstorDefault(e=>e.pokemon_species.name == name);
        }
        [HttpGet("BestTeam")]
        public List<Pokemon_Species> getBestTeam()
        {
            Pokemon_Species bidoof = pokeDex.FirstOrDefault(p => p.pokemon_species.name.ToLower() == "bidoof").pokemon_species;
            List<Pokemon_Species> ultimateTeam = new List<Pokemon_Species>();
            for(int i = 0; i < 6; i++)
            {
                ultimateTeam.Add(bidoof);
            }
            return ultimateTeam;
        }
    }
}
