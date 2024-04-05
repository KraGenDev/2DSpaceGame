using System;
using Systems;
using Interfaces;
using UnityEngine;

namespace Gameplay.Environments
{
    public class Asteroid : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        
        private float _destroyHeight;
        
        public event Action Destroyed;
        

        private void Start()
        {
            _rigidbody ??= GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            ApplicationController.PauseStatusChanged += OnPauseStatusChanged;
            ChangeFallingSpeed();
        }

        private void OnDisable()
        {
            ApplicationController.PauseStatusChanged -= OnPauseStatusChanged;
        }


        public void SetDestroyHeight(float height) => _destroyHeight = height;
        
        private void OnPauseStatusChanged(bool obj) => _rigidbody.simulated = !obj;

        private void ChangeFallingSpeed()
        {
            var difficulty = GameProgression.Instance.Difficulty / 100;
            _rigidbody.gravityScale = difficulty;
        }

        private void Update()
        {
            if (transform.position.y <= _destroyHeight)
                Destroy(false);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent<IDamageable>(out var iDamageable))
            {
                iDamageable.TakeDamage();
                Destroy();
            }
        }

        private void Destroy(bool callEvent = true)
        {
            if(callEvent)
                Destroyed?.Invoke();
            
            gameObject.SetActive(false);
        }
    }
}