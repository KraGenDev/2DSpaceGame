using System;
using UnityEngine;

namespace Interfaces
{
    public interface IInput
    {
        event Action<Vector2> SubmitInput;
    }
}