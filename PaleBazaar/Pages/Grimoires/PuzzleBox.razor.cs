using PaleBazaar.MechanistTower.PuzzleBoxWorkshop;
using System;
using Microsoft.AspNetCore.Components;

namespace PaleBazaar.Pages.Grimoires
{
    public partial class PuzzleBox
    {
        private Type _gameType;
        protected string Game { get; set; }

        // Called when the page is first loaded
        protected override void OnInitialized()
        {
            Game = "Welcome";
            UpdateGameType();
        }

        // Called when the drop-down menu value changes
        protected void UpdateGame(ChangeEventArgs e)
        {
            Game = e.Value.ToString();
            UpdateGameType();
        }

        // Updates the _gameType based on the Game variable
        private void UpdateGameType()
        {
            _gameType = Type.GetType($"PaleBazaar.Pages.Grimoires.PuzzleBoxGames.{Game}, PaleBazaar.dll");
        }
    }
}
