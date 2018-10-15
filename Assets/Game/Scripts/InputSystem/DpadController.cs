using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shinnii.Controller
{
    public class DpadController : MonoBehaviour, IReceiveMovement
    {

        public MovementInput movementInput;
        public GameObject receiver;
        public IReceiveMovement movementReceiver;

        public void SetReceiver(GameObject receiver)
        {
            this.receiver = receiver;

            IReceiveMovement movementReceiver = receiver.GetComponent<IReceiveMovement>();

            if (movementReceiver != null)
                this.movementReceiver = movementReceiver;
        }

        public void OnReceiveMovement(Vector2 direction)
        {
            if (movementReceiver != null)
                movementReceiver.OnReceiveMovement(direction);
        }

    }
}