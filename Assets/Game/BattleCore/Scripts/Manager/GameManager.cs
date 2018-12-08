using System.Collections;
using System.Collections.Generic;
using Shinnii.Controller;
using UnityEngine;

namespace BattleCore
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager instance;
        public static GameManager Instance
        {
            get
            {
                if (instance == null)
                    instance = GameObject.FindObjectOfType<GameManager>();
                return instance;
            }
        }

        public Transform world;
        public DpadController controller;
        public ObjectPoolManager worldCanvasPoolManager;
        public new CameraFollower camera;
        // implement factory letter.
        public Hero heroPrefab;
        [HideInInspector] public Hero player;
        void Awake()
        {
            if (instance == null)
                instance = this;

            player = Instantiate(heroPrefab, world);
            controller.SetReceiver(player.gameObject);
            camera.Initialize(player.transform);
            worldCanvasPoolManager.Initialize();
        }

    }
}
