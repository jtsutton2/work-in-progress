using Microsoft.Maui.Controls;
using PokemonDamageCalculator.Models;
using System;
using System.Linq;

namespace PokemonDamageCalculator
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            // Populate Pickers
            AttackerPicker.ItemsSource = PokemonDatabase.PokemonData.Keys.ToList();
            MovePicker.ItemsSource = PokemonDatabase.MoveData.Keys.ToList();
            DefenderPicker.ItemsSource = PokemonDatabase.PokemonData.Keys.ToList();
        }

        private void OnCalculateDamageClicked(object sender, EventArgs e)
        {
            // Ensure selections are valid
            if (AttackerPicker.SelectedItem == null || MovePicker.SelectedItem == null || DefenderPicker.SelectedItem == null)
            {
                DisplayAlert("Error", "Please select all required fields.", "OK");
                return;
            }

            // Get selections
            string attackerName = AttackerPicker.SelectedItem.ToString();
            string moveName = MovePicker.SelectedItem.ToString();
            string defenderName = DefenderPicker.SelectedItem.ToString();
            string weather = WeatherPicker.SelectedItem?.ToString();

            // Fetch data
            var attacker = PokemonDatabase.PokemonData[attackerName];
            var defender = PokemonDatabase.PokemonData[defenderName];
            var move = PokemonDatabase.MoveData[moveName];

            // Determine attack and defense stats
            int attack = move.Category == "Physical" ? attacker.Attack : attacker.SpecialAttack;
            int defense = move.Category == "Physical" ? defender.Defense : defender.SpecialDefense;

            // Apply STAB
            //add adaptability ability
            double stab = attacker.Types.Contains(move.Type) ? 1.5 : 1.0;

            // Apply weather effects
            //add sand force ability
            double weatherModifier = 1.0;
            if (weather == "Sunny" && move.Type == "Fire") weatherModifier = 1.5;
            if (weather == "Rainy" && move.Type == "Water") weatherModifier = 1.5;

            // Apply critical hit
            double critical = CriticalHitCheckBox.IsChecked ? 1.5 : 1.0;

            // Calculate damage
            double damage = (((2 * 50 / 5 + 2) * move.BasePower * (attack / (double)defense)) / 50 + 2) 
                            * stab * weatherModifier * critical;
            
            //round to nearest int
            // Display result
            ResultLabel.Text = $"Damage: {damage:F2}";
        }
    }
}

//overall want to add more status effects (leech seed, burn, spikes, etc.)
//add random 85-100% factor
//add multi target support
//random edge cases like air lock