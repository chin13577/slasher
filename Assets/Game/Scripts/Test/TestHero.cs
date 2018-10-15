using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shinnii.Controller;
public class TestHero : MonoBehaviour, IReceiveMovement
{
    public DpadController controller;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        controller.SetReceiver(this.gameObject);
    }
    // Update is called once per frame
    void IReceiveMovement.OnReceiveMovement(Vector2 direction)
    {
        print(direction);
    }
}
