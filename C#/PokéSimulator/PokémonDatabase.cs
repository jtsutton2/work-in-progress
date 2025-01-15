using System.Collections.Generic;

namespace PokemonDamageCalculator.Models
{
    public static class PokemonDatabase
    {
        // Pokémon Data: Name -> (Attack, SpecialAttack, Defense, SpecialDefense, Types)
        public static readonly Dictionary<string, (int Attack, int SpecialAttack, int Defense, int SpecialDefense, List<string> Types)> PokemonData =
            new Dictionary<string, (int, int, int, int, List<string>)>
            {
                { "Pikachu", (55, 50, 40, 50, new List<string> { "Electric" }) },
                { "Charizard", (84, 109, 78, 85, new List<string> { "Fire", "Flying" }) },
                { "Blastoise", (83, 85, 100, 105, new List<string> { "Water" }) }
            };

        // Move Data: Name -> (BasePower, Category, Type)
        public static readonly Dictionary<string, (int BasePower, string Category, string Type)> MoveData =
            new Dictionary<string, (int, string, string)>
            {
                { "Thunderbolt", (90, "Special", "Electric") },
                { "Flamethrower", (90, "Special", "Fire") },
                { "Tackle", (40, "Physical", "Normal") }
            };

        //Type Effectiveness Data: Attacking Type -> (Defending Type -> Multiplier)
        public static readonly Dictionary<string, Dictionary<string, double>> Effectiveness =
            new Dictionary<string, Dictionary<string, double>>
            {
                {}
            };
    }
}