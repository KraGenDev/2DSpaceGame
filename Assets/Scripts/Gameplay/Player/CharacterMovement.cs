using Systems;
using Interfaces;
using UnityEngine;

namespace Gameplay.Player
{
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _maxHorizontalPosition;
        [Space]
        [SerializeField] private GameObject InputObject;
        [SerializeField] private Vector2 _targetPosition;

        private IInput _input;
        private Vector2 _moveDirection;
        private bool _pause = false;
        public Vector2 GetTargetPosition => _targetPosition;
        

        private void OnEnable()
        {
            _input ??= InputObject.GetComponent<IInput>();
            
            if(_input != null)
                _input.SubmitInput += OnSubmitInput;
            
            ApplicationController.PauseStatusChanged += OnPauseStatusChanged;
        }

        private void OnDisable()
        {
            if(_input != null)
                _input.SubmitInput -= OnSubmitInput;
            
            ApplicationController.PauseStatusChanged -= OnPauseStatusChanged;
        }


        private void OnPauseStatusChanged(bool value) => _pause = value;

        private void Update()
        {
            if (_pause) return;
            
            Move();
        }

        private void OnSubmitInput(Vector2 input) => _moveDirection = input;

        private void Move()
        {
            var position = transform.position;
            _targetPosition += _moveDirection;
            _targetPosition.x = Mathf.Clamp(_targetPosition.x, -_maxHorizontalPosition, _maxHorizontalPosition);
            transform.position = Vector2.Lerp(position, _targetPosition, _movementSpeed * Time.deltaTime); 
        }
    }
}