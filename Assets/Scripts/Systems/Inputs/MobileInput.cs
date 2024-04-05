using System;
using Interfaces;
using UnityEngine;

namespace Systems.Inputs
{
    public class MobileInput : MonoBehaviour, IInput
    {
        public event Action<Vector2> SubmitInput;

        public void SetHorizontalAxis(float value)
        {
            SubmitInput?.Invoke(new Vector2(value,0));
        }
    }
}