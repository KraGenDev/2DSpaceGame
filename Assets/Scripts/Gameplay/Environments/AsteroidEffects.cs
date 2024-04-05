using UnityEngine;

namespace Gameplay.Environments
{
    public class AsteroidEffects : MonoBehaviour
    {
        [SerializeField] private Asteroid _asteroid;
        [Space] 
        [SerializeField] private ParticleSystem _destroyEffect;

        private void OnEnable()
        {
            if(_asteroid != null)
                _asteroid.Destroyed += OnDestroyed;
        }

        private void OnDisable()
        {
            if(_asteroid != null)
                _asteroid.Destroyed -= OnDestroyed;
        }

        private void OnDestroyed()
        {
            if (_destroyEffect == null) return;
            
            _destroyEffect.transform.SetParent(transform);
            _destroyEffect.transform.localPosition = Vector3.zero;
            _destroyEffect.Play();
            _destroyEffect.transform.SetParent(null);
        }
    }
}