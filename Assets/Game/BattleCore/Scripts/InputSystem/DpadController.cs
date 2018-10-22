using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shinnii.Controller
{
    public class DpadController : MonoBehaviour, IReceiveMovement, IReceiveAttackable
    {

        public MovementInput movementInput;
        public GameObject receiver;
        public IReceiveMovement movementReceiver;
        public IReceiveAttackable attackReceiver;

        public void SetReceiver(GameObject receiver)
        {
            this.receiver = receiver;

            IReceiveMovement movementReceiver = receiver.GetComponent<IReceiveMovement>();
            IReceiveAttackable attackReceiver = receiver.GetComponent<IReceiveAttackable>();

            if (movementReceiver != null)
                this.movementReceiver = movementReceiver;
            if (attackReceiver != null)
                this.attackReceiver = attackReceiver;
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
    }
}