using UnityEngine;

namespace Shinnii.Controller
{
    public interface IReceiveMovement
    {
        void OnReceiveMovement(Vector2 direction);
    }
}