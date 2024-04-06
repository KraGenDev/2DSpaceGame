using System.Collections;
using Systems;
using Gameplay.Environments;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay
{
    public class ObstaclesManager : MonoBehaviour
    {
        [SerializeField] private float _obstacleSpawnDelay; 
        [SerializeField] private float _spawnPointHeigh;
        [SerializeField] private float _horizontalSpawnPositionRange;
        [SerializeField] private float _obstacleDestroyHeight = -3;
        [SerializeField] private float _minObstacleSpawnSpeed = 1.4f;
        [Header("Pool Settings")]
        [SerializeField] private int _countAsteroidsInPool = 10;
        [SerializeField] private Asteroid _asteroidPrefab;
        [SerializeField] private bool _autoExpand = true;
        [SerializeField] private Transform _asteroidsRoot;

        private PoolMono<Asteroid> _asteroidPool;
        private Coroutine _cyclicallySpawn;
        private bool _pause = false;

        private void Awake()
        {
            _asteroidPool = new PoolMono<Asteroid>(_asteroidPrefab, _countAsteroidsInPool, _asteroidsRoot)
            {
                AutoExpand = _autoExpand
            };
        }

        private void Start()
        {
            _cyclicallySpawn = StartCoroutine(CyclicalObstacleSpawn());
        }
        
        private void OnEnable()
        {
            ApplicationController.PauseStatusChanged += OnPauseStatusChanged;
        }

        private void OnDisable()
        {
            ApplicationController.PauseStatusChanged -= OnPauseStatusChanged;
        }

        private void OnPauseStatusChanged(bool obj) => _pause = obj;

        private IEnumerator CyclicalObstacleSpawn()
        {
            while (true)
            {
                yield return new WaitUntil((() => !_pause));
                
                var asteroid = _asteroidPool.GetFreeElement();
                var randomValue = Random.Range(-_horizontalSpawnPositionRange, _horizontalSpawnPositionRange);
                
                asteroid.transform.position = new Vector2(randomValue, _spawnPointHeigh);
                asteroid.SetDestroyHeight(_obstacleDestroyHeight);

                var delay = _obstacleSpawnDelay - DifficultController.Instance.Difficulty / 10;

                yield return new WaitForSeconds(delay < _minObstacleSpawnSpeed
                    ? _minObstacleSpawnSpeed 
                    : delay);
            }
        }
    }
}