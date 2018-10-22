using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shinnii.Controller;
using Shinnii.StateMachine;
using System;

public abstract class Character : MonoBehaviour, IReceiveMovement, IReceiveAttackable
{
    public Status status = new Status();
    public Rigidbody2D rigid;
    [SerializeField] private StateMachineGraph graph;
    private StateMachine machine;
    public Animator animator;
    public List<IReceiveMovement> receiveMovements = new List<IReceiveMovement>();
    public List<IReceiveAttackEnter> receiveAttackEnters = new List<IReceiveAttackEnter>();
    public List<IReceiveAttackDrag> receiveAttackDrags = new List<IReceiveAttackDrag>();
    public List<IReceiveAttackUp> receiveAttackUps = new List<IReceiveAttackUp>();
    void Awake()
    {
        machine = new StateMachine(graph, this);
    }

    #region ListenerHandler

    public void AddListener(IReceiveMovement receiver)
    {
        if (!receiveMovements.Contains(receiver))
            receiveMovements.Add(receiver);
    }
    public void AddListener(IReceiveAttackEnter receiver)
    {
        if (!receiveAttackEnters.Contains(receiver))
            receiveAttackEnters.Add(receiver);
    }
    public void AddListener(IReceiveAttackDrag receiver)
    {
        if (!receiveAttackDrags.Contains(receiver))
            receiveAttackDrags.Add(receiver);
    }
    public void AddListener(IReceiveAttackUp receiver)
    {
        if (!receiveAttackUps.Contains(receiver))
            receiveAttackUps.Add(receiver);
    }
    public void RemoveListener(IReceiveMovement receiver)
    {
        receiveMovements.Remove(receiver);
    }
    public void RemoveListener(IReceiveAttackEnter receiver)
    {
        receiveAttackEnters.Remove(receiver);
    }
    public void RemoveListener(IReceiveAttackDrag receiver)
    {
        receiveAttackDrags.Remove(receiver);
    }
    public void RemoveListener(IReceiveAttackUp receiver)
    {
        receiveAttackUps.Remove(receiver);
    }


    #endregion
    public void Move(Vector2 direction, float power)
    {
        Vector3 newPos = this.transform.position + new Vector3(direction.x, direction.y, 0) * status.speed * power * Time.deltaTime;
        rigid.MovePosition(newPos);
        UpdateSprite(direction);
    }

    private void UpdateSprite(Vector2 direction)
    {
        if (direction.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (direction.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
    }


    #region  ReceiveEvent
    public void OnReceiveMovement(Vector2 direction, float power)
    {
        for (int i = 0; i < receiveMovements.Count; i++)
            receiveMovements[i].OnReceiveMovement(direction, power);
    }

    void IReceiveAttackEnter.OnReceiveAttackEnter()
    {
        for (int i = 0; i < receiveAttackEnters.Count; i++)
            receiveAttackEnters[i].OnReceiveAttackEnter();
    }

    void IReceiveAttackDrag.OnReceiveAttackDrag(Vector2 direction)
    {
        for (int i = 0; i < receiveAttackDrags.Count; i++)
            receiveAttackDrags[i].OnReceiveAttackDrag(direction);
    }

    void IReceiveAttackUp.OnReceiveAttackUp()
    {
        for (int i = 0; i < receiveAttackUps.Count; i++)
            receiveAttackUps[i].OnReceiveAttackUp();
    }
    #endregion
}