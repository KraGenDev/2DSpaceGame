using Systems;
using UnityEngine;

namespace Gameplay.Player
{
    public class ShipEngines : MonoBehaviour
    {
        [SerializeField] private ParticleSystem[] _engines;

        private float _defaultSimulationSpeed;
        private bool _hasEngines = false;
        
        private void OnEnable()
        {
            ApplicationController.PauseStatusChanged += OnPauseStatusChanged;

            if (_engines[0] != null)
            {
                _defaultSimulationSpeed = _engines[0].main.simulationSpeed;
                _hasEngines = true;
            }
        }

        private void OnDisable()
        {
            ApplicationController.PauseStatusChanged -= OnPauseStatusChanged;
        }

        private void OnPauseStatusChanged(bool value)
        {
            if (!_hasEngines) return;
            
            foreach (var engine in _engines)
            {
                var engineMain = engine.main;
                engineMain.simulationSpeed = value 
                    ? 0 
                    : _defaultSimulationSpeed;
            }
        }

    }
}