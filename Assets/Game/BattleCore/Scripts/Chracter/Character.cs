using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shinnii.Controller;
using Shinnii.StateMachine;
using System;

public abstract class Character : MonoBehaviour, IReceiveMovement, IReceiveAttackEvent, IReceiveDashEvent
{
    [Header("Component")]
    public Rigidbody2D rigid;
    public Animator animator;
    public Weapon weapon;
    public Transform handTransform;
    [Header("UI")]
    public Transform directionTransform;
    [Header("Data")]
    public Status status = new Status();
    [SerializeField] private StateMachineGraph graph;
    private StateMachine machine;

    private Vector2 _direction = new Vector2(1, 0);
    public Vector2 Direction { get { return _direction; } set { _direction = value; } }
    private float currentPower;

    public List<IReceiveMovement> receiveMovements = new List<IReceiveMovement>();
    public List<IReceiveDashEvent> receiveDashEvents = new List<IReceiveDashEvent>();
    public List<IReceiveAttackEnter> receiveAttackEnters = new List<IReceiveAttackEnter>();
    public List<IReceiveAttackDrag> receiveAttackDrags = new List<IReceiveAttackDrag>();
    public List<IReceiveAttackUp> receiveAttackUps = new List<IReceiveAttackUp>();
    public float dashSpeed { get { return status.speed * 3; } }
    public bool IsDash { get; set; }

    void Awake()
    {
        machine = new StateMachine(graph, this);
        UpdateSprite(Direction);
        UpdateDirectionSprite(Direction);
        rigid.isKinematic = true;
    }

    void Update()
    {
        machine.Update();
        rigid.isKinematic = currentPower == 0 && !IsDash;
    }

    #region ListenerHandler

    public void AddListener(IReceiveMovement receiver)
    {
        if (!receiveMovements.Contains(receiver))
            receiveMovements.Add(receiver);
    }
    public void AddListener(IReceiveDashEvent receiver)
    {
        if (!receiveDashEvents.Contains(receiver))
            receiveDashEvents.Add(receiver);
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
    public void RemoveListener(IReceiveDashEvent receiver)
    {
        receiveDashEvents.Remove(receiver);
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

    public void Rotate(Vector2 direction)
    {
        Direction = direction;
        UpdateSprite(Direction);
        UpdateDirectionSprite(Direction);
        UpdateHandTransformRotation(Direction);
    }
    public void Move(float power)
    {
        currentPower = power;
        Vector3 newPos = this.transform.position + new Vector3(Direction.x, Direction.y, 0) * status.speed * power * Time.deltaTime;
        rigid.MovePosition(newPos);
    }

    private void UpdateSprite(Vector2 direction)
    {
        if (direction.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (direction.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
    }
    private void UpdateDirectionSprite(Vector2 direction)
    {
        directionTransform.rotation = CalculateRotationFromDirection(direction);
    }

    private void UpdateHandTransformRotation(Vector2 direction)
    {
        handTransform.rotation = CalculateRotationFromDirection(direction);
    }

    private Quaternion CalculateRotationFromDirection(Vector2 direction)
    {
        float angle = 0;
        if (direction.x > 0)
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        else
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 180f;

        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        rotation = Quaternion.Euler(20, rotation.eulerAngles.y, rotation.eulerAngles.z);
        return rotation;
    }

    #region  ReceiveEvent
    public void OnReceiveMovement(Vector2 direction, float power)
    {
        for (int i = 0; i < receiveMovements.Count; i++)
            receiveMovements[i].OnReceiveMovement(direction, power);
    }

    void IReceiveDashEvent.OnReceiveDashEvent()
    {
        for (int i = 0; i < receiveDashEvents.Count; i++)
            receiveDashEvents[i].OnReceiveDashEvent();
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

    #region CallbackAnimationEvent
    public virtual void OnAnimationAttackTrigger(int param) { }

    #endregion
}