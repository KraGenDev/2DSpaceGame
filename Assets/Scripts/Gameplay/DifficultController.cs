using UnityEngine;

namespace Gameplay
{
    public class DifficultController : MonoBehaviour
    {
        [SerializeField] private float _progressionUpdateStep = 2f;
        [SerializeField] private float _maxDifficulty = 20;
        [SerializeField] private float _difficultyStep = 0.5f;
        
        private float _timer;

        public float Difficulty { get; private set; } = 1;
        public float MaxDifficulty => _maxDifficulty;
        
        public static DifficultController Instance;
        

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        private void Update()
        {
            _timer += Time.deltaTime;

            if (_timer >= _progressionUpdateStep)
            {
                _timer = 0;
                AddDifficulty();
            }
        }
        
        private void AddDifficulty()
        {
            if (Difficulty >= _maxDifficulty) return;
            
            Difficulty += _difficultyStep;
        }
    }
}