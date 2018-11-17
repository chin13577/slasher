using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shinnii.Senses;
using System;

public class SensorController : MonoBehaviour
{
    public delegate void OnColliderTrigger(Collider2D other);
    public event OnColliderTrigger OnTriggerEnter;
    public event OnColliderTrigger OnTriggerExit;
    [HideInInspector]
    public Character character;
    public LayerMask mask;
    public new CircleCollider2D collider;
    public List<Sense> senses = new List<Sense>();

    public void Initialize(Character character)
    {
        this.character = character;
        foreach (Sense sense in senses)
        {
            sense.Initialize(this);
        }
    }
    void Update()
    {
        foreach (Sense sense in senses)
        {
            sense.OnUpdate();
        }
    }

    void OnDestroy()
    {
        foreach (Sense sense in senses)
        {
            sense.Dispose();
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Character characterComponent = other.GetComponent<Character>();
        if (characterComponent == null) return;
        if (characterComponent == character) return;
        if (characterComponent.team == character.team) return;

        if (OnTriggerEnter != null)
            OnTriggerEnter(other);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Character characterComponent = other.GetComponent<Character>();
        if (characterComponent == null) return;
        if (characterComponent == character) return;
        if (characterComponent.team == character.team) return;

        if (OnTriggerExit != null)
            OnTriggerExit(other);
    }

}
