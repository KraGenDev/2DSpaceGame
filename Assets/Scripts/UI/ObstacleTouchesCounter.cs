using DG.Tweening;
using Gameplay.Player;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ObstacleTouchesCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _counterText;
        [SerializeField] private CharacterInteractions _characterInteractions;

        private bool _animated = false;
        
        private void OnEnable()
        {
            if(_characterInteractions != null)
                _characterInteractions.ObstacleTouched += OnObstacleTouched;
        }

        private void OnDisable()
        {
            if(_characterInteractions != null)
                _characterInteractions.ObstacleTouched -= OnObstacleTouched;
        }

        private void OnObstacleTouched()
        {
            _counterText.text = _characterInteractions.ObstacleTouchCount.ToString();

            if (!_animated)
            {
                _animated = true;
                _counterText.color = Color.red;
                _counterText.transform
                    .DOShakeScale(0.15f, 1, 5)
                    .OnComplete((() =>
                    {
                        _counterText.color = Color.white;
                        _animated = false;
                    }));
            }
        }
    }
}