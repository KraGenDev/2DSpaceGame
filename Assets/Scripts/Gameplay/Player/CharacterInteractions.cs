using System;
using Interfaces;
using UnityEngine;

namespace Gameplay.Player
{
    public class CharacterInteractions : MonoBehaviour, IDamageable
    {
        public int ObstacleTouchCount { get; private set; }

        public event Action ObstacleTouched;
        
        public void TakeDamage(int damage)
        {
            ObstacleTouchCount++;
            ObstacleTouched?.Invoke();
        }
    }
}