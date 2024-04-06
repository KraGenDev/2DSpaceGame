using Analytics;
using Gameplay.Player;
using TMPro;
using UnityEngine;

namespace UI.Windows
{
    public class PauseWindow : AnimatedWindow
    {
        [Space]
        [SerializeField] private TextMeshProUGUI _obstaclesCounter;
        [SerializeField] private CharacterInteractions _characterInteractions;
        
        public override void Show()
        {
            _obstaclesCounter.text = _characterInteractions != null
                ? _characterInteractions.ObstacleTouchCount.ToString()
                : ":(";
            
            GameAnalytics.Instance.Score(_characterInteractions.ObstacleTouchCount);
            base.Show();
        }
    }
}