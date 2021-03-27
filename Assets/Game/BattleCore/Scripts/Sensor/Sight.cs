using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Shinnii.Senses
{

    public class Sight : Sense
    {
        [Header("Setting")]
        [SerializeField] float halfFov = 30;
        [SerializeField] bool isUseFov;
        [SerializeField] bool isUseMinDist;
        private Character character;
        private List<GameObject> gameObjects = new List<GameObject>();

        private GameObject target;
        public override void Initialize(SensorController controller)
        {
            base.controller = controller;
            controller.OnTriggerEnter += OnColliderEnter;
            controller.OnTriggerExit += OnColliderExit;
            character = controller.character;
        }
        public override void OnUpdate()
        {
            var obj = FindTrackingObject();
            if (target != obj)
            {
                target = obj;
                character.TargetOnSight = FindTrackingObject();
                controller.ObserveChange(obj);
            }
        }

        private GameObject FindTrackingObject()
        {
            if (gameObjects.Count == 0) return null;
            GameObject target = null;
            if (isUseFov)
                target = FindTargetInFov();
            if (target == null && isUseMinDist)
                target = FindNearestTarget();
            // if (target == null && character.TrackingTarget != null && gameObjects.Contains(character.TrackingTarget))
            //     target = character.TrackingTarget;

            return target;
        }

        private GameObject FindTargetInFov()
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                if (Vector2.Angle(character.Direction, (gameObjects[i].transform.position - character.transform.position).ToVector2()) <= halfFov)
                {
                    return gameObjects[i];
                }
            }
            return null;
        }

        private GameObject FindNearestTarget()
        {
            float minDist = float.MaxValue;
            int index = 0;
            for (int i = 0; i < gameObjects.Count; i++)
            {
                float sqrMagnitude = Vector2.SqrMagnitude(gameObjects[i].transform.position - character.transform.position);
                if (sqrMagnitude < minDist)
                {
                    minDist = sqrMagnitude;
                    index = i;
                }
            }
            return gameObjects[index];
        }

        public override void Dispose()
        {
            controller.OnTriggerEnter -= OnColliderEnter;
            controller.OnTriggerExit -= OnColliderExit;
        }

        private void OnColliderEnter(Collider2D other)
        {
            if (gameObjects.Contains(other.gameObject)) return;
            gameObjects.Add(other.gameObject); 
        }

        private void OnColliderExit(Collider2D other)
        {
            if (!gameObjects.Contains(other.gameObject)) return;
            gameObjects.Remove(other.gameObject); 
        }

    }
}
