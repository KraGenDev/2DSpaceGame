using UnityEngine;

namespace Gameplay.Player
{
    public class CharacterModelRotator : MonoBehaviour
    {
        [SerializeField] private CharacterMovement _characterMovement;
        [SerializeField] private GameObject _model;
        [Space] 
        [SerializeField] private float _maxAngle;
        [SerializeField] private float _maxDistance;
        [SerializeField] private float _minDistanceForRotation = 0.1f;

        private void Update()
        {
            Rotate();
        }

        private void Rotate()
        {
            var position = transform.position;
            var distanceToTargetPosition = Vector3.Distance(position, _characterMovement.GetTargetPosition);
            var angle = 0f;

            if (distanceToTargetPosition > _minDistanceForRotation)
            {
                var t = Mathf.Clamp01((distanceToTargetPosition - _minDistanceForRotation) / (_maxDistance - _minDistanceForRotation));
                angle = Mathf.LerpAngle(0f, _maxAngle, t);
                
                var direction = (_characterMovement.GetTargetPosition - (Vector2)position).normalized;

                if (direction.x < 0)
                    angle *= -1;
            }
            
            _model.transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
    }
}