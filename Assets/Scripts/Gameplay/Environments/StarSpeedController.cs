using System;
using Systems;
using UnityEngine;

namespace Gameplay.Environments
{
    public class StarSpeedController : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _star;
        [SerializeField] private float _maxSimulationSpeed = 1;

        private bool _pause = false;

        
        private void OnEnable()
        {
            ApplicationController.PauseStatusChanged += OnPauseStatusChanged;
        }

        private void OnDisable()
        {
            ApplicationController.PauseStatusChanged -= OnPauseStatusChanged;
        }

        private void OnPauseStatusChanged(bool value)
        {
            _pause = value;

            if (_pause)
            {
                var mainModule = _star.main;
                mainModule.simulationSpeed = 0;
            }
        }

        private void Update()
        {
            if (_pause) return;

            var difficulty = DifficultController.Instance.Difficulty;
            var maxDifficulty = DifficultController.Instance.MaxDifficulty;
            var mainModule = _star.main;
            mainModule.simulationSpeed = difficulty / maxDifficulty;
        }
    }
}