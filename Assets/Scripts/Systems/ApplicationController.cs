using System;
using UnityEngine;

namespace Systems
{
    public class ApplicationController : MonoBehaviour
    {
        private bool _pause;

        public static event Action<bool> PauseStatusChanged;

        public bool Pause
        {
            get => _pause;
            set
            {
                if (value != _pause)
                {
                    _pause = value;
                    PauseStatusChanged?.Invoke(_pause);
                }
            }
        }

        private void Awake()
        {
            Input.multiTouchEnabled = false;
            Application.targetFrameRate = 60;
        }

        public void SetPauseStatus(bool value) => Pause = value;

        public void Quit() => Application.Quit();
    }
}