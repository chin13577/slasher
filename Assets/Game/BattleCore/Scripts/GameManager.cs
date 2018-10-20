using System.Collections;
using System.Collections.Generic;
using Shinnii.Controller;
using UnityEngine;

namespace BattleCore
{
    public class GameManager : MonoBehaviour
    {
        public DpadController controller;
        // implement factory letter.
        public Hero heroPrefab;

        void Awake()
        {
            var hero = Instantiate(heroPrefab);
            controller.SetReceiver(hero.gameObject);
        }

    }
}
