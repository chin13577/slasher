using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shinnii.Controller
{
    public class DpadController : MonoBehaviour, IReceiveMovement, IReceiveAttackEvent, IReceiveDashEvent
    {

        public MovementInput movementInput;
        public GameObject receiver;
        private IReceiveMovement movementReceiver;
        private IReceiveAttackEvent attackReceiver;
        private IReceiveDashEvent dashReceiver;

        public void SetReceiver(GameObject receiver)
        {
            this.receiver = receiver;

            IReceiveMovement movementReceiver = receiver.GetComponent<IReceiveMovement>();
            IReceiveAttackEvent attackReceiver = receiver.GetComponent<IReceiveAttackEvent>();
            IReceiveDashEvent dashReceiver = receiver.GetComponent<IReceiveDashEvent>();

            this.movementReceiver = movementReceiver;
            this.attackReceiver = attackReceiver;
            this.dashReceiver = dashReceiver;
        }

        public void OnReceiveMovement(Vector2 direction, float power)
        {
            if (movementReceiver != null)
                movementReceiver.OnReceiveMovement(direction, power);
        }

        public void OnReceiveAttackEnter()
        {
            if (attackReceiver != null)
                attackReceiver.OnReceiveAttackEnter();
        }

        public void OnReceiveAttackDrag(Vector2 direction)
        {
            if (attackReceiver != null)
                attackReceiver.OnReceiveAttackDrag(direction);
        }

        public void OnReceiveAttackUp()
        {
            if (attackReceiver != null)
                attackReceiver.OnReceiveAttackUp();
        }

         public void OnReceiveDashEvent()
        {
            if (dashReceiver != null)
                dashReceiver.OnReceiveDashEvent();
        }
    }
}