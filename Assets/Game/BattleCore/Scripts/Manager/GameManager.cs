using System.Collections;
using System.Collections.Generic;
using Shinnii.Controller;
using UnityEngine;

namespace BattleCore
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        public Transform world;
        public DpadController controller;
        public ObjectPoolManager worldCanvasPoolManager;
        // implement factory letter.
        public Hero heroPrefab;

        void Awake()
        {
            instance = this;

            var hero = Instantiate(heroPrefab, world);
            controller.SetReceiver(hero.gameObject);
            worldCanvasPoolManager.Initialize();
        }

    }
}
