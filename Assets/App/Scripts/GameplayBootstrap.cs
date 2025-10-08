using System.Collections.Generic;
using App.EcsPausing;
using App.LevelUpManagement;
using App.Perks;
using App.Perks.PerksManagement;
using UnityEngine;
using Zenject;

namespace App
{
    public class GameplayBootstrap : MonoBehaviour
    {
        [SerializeField] private PerksChooseWindow perksChooseWindow;

        [Inject] private readonly LevelUpManager _levelUpManager;
        
        private void Start()
        {
            _levelUpManager.LevelUp();
        }
    }
}